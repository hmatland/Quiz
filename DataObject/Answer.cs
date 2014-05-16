using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Answer
    {
        public long ID { get; set; }
        public string answerText { get; set; }
        public Boolean isCorrect { get; set; }
        public long questionID { get; set; }

        public override string ToString()
        {
            return answerText+": "+isCorrect;
        }
    }
}
