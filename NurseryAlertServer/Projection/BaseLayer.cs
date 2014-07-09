﻿/*
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

namespace Pbp
{
    public abstract class BaseLayer
    {
        public void writeOut(System.Drawing.Graphics gr)
        {
            Object[] args = { };
            writeOut(gr, args, ProjectionMode.Projection);
        }

        public void writeOut(System.Drawing.Graphics gr, Object[] args)
        {
            writeOut(gr, args, ProjectionMode.Projection);
        }

        public abstract void writeOut(System.Drawing.Graphics gr, Object[] args, ProjectionMode pm);
    }

    public enum ProjectionMode
    {
        Simulate,
        Projection
    }
}