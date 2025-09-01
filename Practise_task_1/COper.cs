using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise_task_1
{
    public class COper
    {
        private int a;
        private int b;
        private string operation;
        private int result;
        public COper() { }
        public COper(int a, int b, string operation, int result)
        {
            this.a = a;
            this.b = b;
            this.operation = operation;
            this.result = result;
        }

        public int A
        {
            get { return a; }
            set { a = value; }
        }

        public int B
        {
            get { return b; }
            set { b = value; }
        }

        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        public int Result
        {
            get { return result; }
            set { result = value; }
        }
    }
}
