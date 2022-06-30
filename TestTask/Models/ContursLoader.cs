using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;

namespace TestTask.Models
{
    class ContursLoader
    {
        public static List<Contur> LoadJson(string path)
        {
            List<Contur> conturs = new List<Contur>();

            try
            {
                var file = File.ReadAllText(path);
                conturs = JsonSerializer.Deserialize<List<Contur>>(file);
            }
            catch (Exception exp)
            {
                    MessageBox.Show(exp.Message);
            }
               
            return conturs;
        }
    }
}
