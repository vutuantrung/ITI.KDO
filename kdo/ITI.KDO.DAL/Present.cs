﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.KDO.DAL
{
    public class Present
    {
        public int PresentId { get; set; }

        public string PresentName { get; set; }

        public float Price { get; set; }

        public string LinkPresent { get; set; }

        public byte[] Picture { get; set; }

        public int CategoryPresentId { get; set; }

        public int UserId { get; set; }

    }
}
