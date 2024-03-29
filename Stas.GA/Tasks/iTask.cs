﻿using System.Diagnostics;
using System.Drawing;
using V2 = System.Numerics.Vector2;



namespace Stas.GA;
public class MapTask : iTask {
    public override V2 from => V2.Zero;
    public MapTask(uint _id, V2 _to, string _info) : base(_id, _to, _info) {
    }
}
public class MyTask : iTask {
    public override V2 from => new V2(ui.me.pos.X, ui.me.pos.Y) * ui.worldToGridScale;
    public MyTask(uint _id, V2 _to, string _info) : base(_id, _to, _info) {
    }
}
public class EnemyTask : iTask {
    V2 _from;
    public override V2 from => _from;
    public EnemyTask(uint _id, V2 from, V2 _to, string _info) : base(_id, _to, _info) {
        _from = from;
    }
}
public abstract class iTask {
    public int line { get; set; } = 1;
    public abstract V2 from { get; }
    public V2 to { get; set; }
    public uint id { get; }
    public string info { get; set; }
    public Color color { get; set; } = Color.Gray;

    public iTask(uint _id, V2 _to, string _info) {
        Debug.Assert(_id != 0 && _to != V2.Zero && !string.IsNullOrEmpty(_info));
        id = _id;
        to = _to;
        info = _info;
    }
    public override string ToString() {
        return info;
    }
}
