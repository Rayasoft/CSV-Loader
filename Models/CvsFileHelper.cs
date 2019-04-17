using CoreCSVImport.Lib.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCSVImport.Models
{
    public interface ICvsFileHelper
    {
        //string MapCSVFile(string fileSaveWithPath, int Category);
        Task<List<Object>> MapCSVFile(string fileSaveWithPath, int Category);

        DataTable ReadCsvFile(string fileSaveWithPath);

    }
    public class CvsFileHelper : ICvsFileHelper
    {
        public CvsFileHelper() { }
        public async Task<List<Object>> MapCSVFile(string fileSaveWithPath, int category)
        {
            DataTable dt = ReadCsvFile(fileSaveWithPath);
            CSVEntities csvEntity = new CSVEntities();
            List<Object> returnObjectList = new List<object>();
            if(category > 0)
            {
                Mapper<Conference> conferenceMapper = new Mapper<Conference>();
                List<Conference> conferences = conferenceMapper.Map(dt);
                returnObjectList.Add(conferences);
            }
            if (category > 1)
            {
                Mapper<Session> sessionMapper = new Mapper<Session>();
                List<Session> sessions = sessionMapper.Map(dt);
                returnObjectList.Add(sessions);
            }
            return returnObjectList;
        }
        /// <summary>
        /// convert csv to data table
        /// </summary>
        /// <param name="fileSaveWithPath"></param>
        /// <returns></returns>
        public DataTable ReadCsvFile(string fileSaveWithPath)
        {
            DataTable dtCsv = new DataTable();
            string Fulltext;
            try
            {
                if (!String.IsNullOrEmpty(fileSaveWithPath))
                {
                    using (StreamReader sr = new StreamReader(fileSaveWithPath))
                    {
                        while (!sr.EndOfStream)
                        {
                            Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                            Fulltext = Fulltext.Replace("\r","");
                            string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                            for (int i = 0; i < rows.Count() - 1; i++)
                            {
                                string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                                {
                                    if (i == 0)
                                    {
                                        for (int j = 0; j < rowValues.Count(); j++)
                                        {
                                            dtCsv.Columns.Add(rowValues[j]); //add headers  
                                        }
                                    }
                                    else
                                    {
                                        DataRow dr = dtCsv.NewRow();
                                        for (int k = 0; k < dtCsv.Columns.Count; k++)
                                        {
                                            dr[k] = rowValues[k].ToString();
                                        }
                                        dtCsv.Rows.Add(dr); //add other rows  
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw new InvalidDataException("CSVFile content is not correct ");
            }
            return dtCsv;
        }
    }
}
