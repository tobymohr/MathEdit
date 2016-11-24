﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    public class FractionModel : IOperation
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<EnabledFlowDocument> boxes;
        public double width;

        double IOperation.width
        {
            get
            {
                return width;
            }

            set
            {
                this.width = value;
            }
        }

        public FractionModel()
        {
            boxes = new List<EnabledFlowDocument>();
            boxes.Add(new EnabledFlowDocument());
            boxes.Add(new EnabledFlowDocument());
        }

        public List<EnabledFlowDocument> getBoxes()
        {
            return boxes;
        }


        public void onIOperationChanged()
        {
            throw new NotImplementedException();
        }


        
    }
}
