using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionTestBoxTool
{
    class TestItem
    {
        //item name
        public string ItemName { get; set; }
        public Param.TestResult   TestResult { get; set; }
        public Param.TestItemStatus TestItemStatus { get; set; }
    }
}
