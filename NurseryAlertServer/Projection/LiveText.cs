/*
 *   PraiseBase Presenter
 *   The open source lyrics and image projection software for churches
 *
 *   http://code.google.com/p/praisebasepresenter
 *
 *   This program is free software; you can redistribute it and/or
 *   modify it under the terms of the GNU General Public License
 *   as published by the Free Software Foundation; either version 2
 *   of the License, or (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program; if not, write to the Free Software
 *   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 *   Author:
 *      Nicolas Perrenoud <nicu_at_lavine.ch>
 *   Co-authors:
 *      ...
 *
 */

using System;
using System.Drawing;
using NurseryAlertServer.Properties;

namespace NurseryAlertServer.Projection
{
    internal class LiveText : TextLayer
    {
        public string Text { get; set; }

        public StringAlignment HorizontalAlign { get; set; }

        public StringAlignment VerticalAlign { get; set; }

        public int Xpadding { get; set; }

        public int Ypadding { get; set; }

        public float FontSize { get; set; }

        public LiveText()
            : this("")
        {
        }

        public LiveText(string initText)
        {
            Text = initText;
            FontSize = Settings.Default.ProjectionMasterFont.Size;
            HorizontalAlign = StringAlignment.Far;
            VerticalAlign = StringAlignment.Far;
            Xpadding = 10;
            Ypadding = -10;
        }

        public override void writeOut(System.Drawing.Graphics gr, object[] args, ProjectionMode pr)
        {
            Font font = new Font(Settings.Default.ProjectionMasterFont.FontFamily, FontSize, Settings.Default.ProjectionMasterFont.Style);
            Brush fontBrush = new SolidBrush(Settings.Default.ProjectionMasterFontColor);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = HorizontalAlign;

            int w = (int)gr.VisibleClipBounds.Width;
            int h = (int)gr.VisibleClipBounds.Height;

            string[] lines = Text.Split(Environment.NewLine.ToCharArray());

            //TODO: Text size calculation seems to be off by ~24 pixels
            SizeF coveredArea = gr.MeasureString(Text, font);

            int x, y;
            if (strFormat.Alignment == StringAlignment.Far)
                x = w - Xpadding;
            else if (strFormat.Alignment == StringAlignment.Center)
                x = w / 2;
            else
                x = Xpadding;

            if (VerticalAlign == StringAlignment.Far)
                y = h - Ypadding - (int)coveredArea.Height;
            else if (VerticalAlign == StringAlignment.Center)
                y = (h / 2) - (int)(coveredArea.Height / 2);
            else
                y = Ypadding;

            foreach (string l in lines)
            {
                SizeF lm = gr.MeasureString(l, font);

                if (lm.Width > (float)(w - Xpadding))
                {
                    int nc = l.Length / ((int)Math.Ceiling(lm.Width / (float)(w - Xpadding)));
                    string s = string.Join(Environment.NewLine, StringUtils.Wrap(l, nc)).Trim();
                    drawString(gr, s, x, y, font, fontBrush, strFormat);
                    y += (int)gr.MeasureString(s, font).Height + Settings.Default.ProjectionMasterLineSpacing;
                }
                else
                {
                    drawString(gr, l, x, y, font, fontBrush, strFormat);
                    y += (int)lm.Height + Settings.Default.ProjectionMasterLineSpacing;
                }
            }
        }
    }
}