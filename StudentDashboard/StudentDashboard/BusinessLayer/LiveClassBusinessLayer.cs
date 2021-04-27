using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.BusinessLayer
{
    public class LiveClassBusinessLayer
    {
        public LiveClassBusinessLayer()
        {

        }

        public long GetClassroomIdFromSku(string ClassroomSku)
        {
            long ClassroomId = -1;
            try
            {
                string[] array = ClassroomSku.Split('_');
                ClassroomId=long.Parse(array[array.Length-1]);
            }
            catch(Exception Ex)
            {

            }
            return ClassroomId;
        }
    }
}