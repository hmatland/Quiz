using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class QuestionWithAnswers
    {
        public long ID { get; set; }
        public string questionText { get; set; }
        public List<Answer> answers { get; set; }
    }
}
