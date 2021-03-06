﻿using System;
using System.Collections.Generic;

namespace DMod.Models
{
    public class Loot
    {
        // Classification information
        public string Id { get; set; }
        public string Owner { get; set; }

        // General
        public List<Inventory> Items { get; set; }
        public int Credits { get; set; }
        public int Type { get; set; } = 0;
        // 0 = Normal
        // 1 = Special
        // 2 = Astral

        // Global dropped inventory data
        public DateTime Dropped { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
