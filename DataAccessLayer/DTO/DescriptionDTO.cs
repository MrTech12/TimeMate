﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTO
{
    public class DescriptionDTO
    {
        private string description;

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}
