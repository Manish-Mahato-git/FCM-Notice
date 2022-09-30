using FCM_Notice.Modules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCM_Notice.Repositories
{
    public class FCMRepository
    {
        public SqlConnection SQL_Connection()
        {
            //var dbDetail = await ComicProcessor.LoadComic();
            //ReadJesonFromFile();

            SqlConnection SqlC = new SqlConnection(@"Data Source = heartsun.redirectme.net,1424; Integrated Security=False; Initial Catalog=AGKPDB7980; User ID=hadmin; Password=Heart@203906;");
            return SqlC;
        }
        ApiModuel db;
        //public void ReadJesonFromFile()
        //{
        //    StreamReader sr = new StreamReader(@"AA.txt");
        //    string jesonString;
        //    while ((jesonString = sr.ReadLine()) != null)
        //    {
        //        db = JsonConvert.DeserializeObject<ApiModuel>(jesonString);
        //    }
        //}
        public int InsertIntotbl(string stmt)
        {
            SqlConnection sqlcon = SQL_Connection();
            sqlcon.Open();
            SqlTransaction trns = sqlcon.BeginTransaction();
            SqlCommand cmd = sqlcon.CreateCommand();
            cmd.Transaction = trns;
            cmd.CommandText = stmt;
            int a = cmd.ExecuteNonQuery();
            trns.Commit();
            sqlcon.Close();
            return a;
        }
        public List<TblNotice> makeList(string stmt)
        {
            try
            {
                SqlConnection sqlcon = SQL_Connection();
                List<TblNotice> list = new List<TblNotice>();
                TblNotice all=new TblNotice();
                all.name = "All";
                all.memId = 0;
                all.fcmToken = "/topics/agyaulikhanepani";
                list.Add(all);
                sqlcon.Open();
                //string q = "select ServiceName from tblServices";
                SqlCommand cmd = new SqlCommand(stmt, sqlcon);
                SqlDataReader re = cmd.ExecuteReader();
                while (re.Read())
                {
                    //cmboUnit.Items.Add(re[fname]);
                    TblNotice member = new TblNotice();
                    member.name = re["MemName"].ToString();
                    member.memId = Convert.ToInt32(re["MemberID"]);
                    member.fcmToken = re["FCMtoken"].ToString();
                    list.Add(member);
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hypermart");
                return null;
            }
        }
    }
}
