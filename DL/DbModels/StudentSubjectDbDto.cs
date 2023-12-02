using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DbModels

{
    public class StudentSubjectDbDto
    {
        [Key]
        public int StudentSubjecId { get; set; }

        public double GPA { get; set; }

        public int Marks { get; set; }

        public int studentID { get; set;}
        public StudentDbDto? studentDbDto { get; set; }

        public int subjectId { get; set; }
    
        public SubjectDbDto? SubjectDbDto { get; set; }
       
     

    }
}
