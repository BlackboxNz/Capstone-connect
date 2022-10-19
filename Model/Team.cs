﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Connect.Model
{
	public class Team
    {
        [Key]
        public int ID { get; set; }
        public string TeamName { get; set; }
        public ICollection<Visitor> Members { get; set; }
    }
}