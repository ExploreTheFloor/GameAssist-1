﻿#region using
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading;
using ImGuiNET;
using Color = System.Drawing.Color;
using sysd = System.Drawing;
using System.Text;
using System.Drawing;
using System.Linq;
using V3 = System.Numerics.Vector3;
using V2 = System.Numerics.Vector2;
using System.Diagnostics;
#endregion

namespace Stas.GA {
    partial class DrawMain {
        void DrawDebugMapItem(ImDrawListPtr bptr, aMapItem di) {
            Debug.Assert(di != null);
            var rm = ui.MTransform();
            var info = di.info;
            if (!string.IsNullOrEmpty(di.info))
                info = "Error info";
            if (di is StaticMapItem)
                info += " [Static]";
            var s = ImGui.CalcTextSize(info) / 2;
            //fgDraw.AddRectFilled(mapCenter + fpos - s, mapCenter + fpos + s,   ImGuiHelper.Color(0, 0, 0, 255));
            //fgDraw.AddText(mapCenter + fpos - s, ImGuiHelper.Color(255, 128, 128, 255),   fitName);
            var mi_gpos = di.pos * ui.worldToGridScale;
            var his = di.size / 2;
            his *= ui.sett.map_scale;
            var w = info.Length * his;
            var pos = V2.Transform(new V2(mi_gpos.X, mi_gpos.Y), rm);
            var lt = pos.Increase(-his, -his - 5);
            var rt = pos.Increase(his + w, -his - 5);
            var lb = pos.Increase(-his, his + 5);
            var rb = pos.Increase(his + w, his + 5);

            map_ptr.AddQuadFilled(lt, rt, rb, lb, Color.Green.ToImgui());
            map_ptr.AddQuad(lt, rt, rb, lb, Color.LightGreen.ToImgui(), 2f);
            //info = "d=" + Math.Round(ui.me.Pos.GetDistance(ami.pos) * ui.worldToGridScale, 0) + " " + ami.info;
            var _draw = true;
            var tcolor = Color.Black.ToImgui();
            if (di is StaticMapItem) {
                var smi = (StaticMapItem)di;
                if (smi.remn != null) {
                    _draw = false;
                    var pinfo = "";
                    foreach (var v in smi.remn.positive)
                        pinfo += v.Key + " ";
                    bptr.AddText(pos.Increase(15, -15), tcolor, pinfo);
                    var ninfo = "";
                    foreach (var v in smi.remn.negative)
                        ninfo += v.Key + " ";
                    bptr.AddText(pos.Increase(15, 0), tcolor, ninfo);
                }
            }
            if(_draw)
                bptr.AddText(pos.Increase(15, 0), tcolor, info);

        }
    }
}
