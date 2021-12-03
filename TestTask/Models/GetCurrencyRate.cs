using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace TestTask.Models
{
    public class GetCurrencyRate
    {

        public static void LoadCurrencyRate(ApplicationContext db)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("https://www.cbr.ru/scripts/XML_daily.asp");
                DateTime dateTime = Convert.ToDateTime(xml.DocumentElement.Attributes[0].Value);
                foreach (XmlElement item in xml.DocumentElement)
                {
                    if (!(db.currencyRates.Any<CurrencyRate>(p => p.CurrencyId == item.Attributes[0].InnerText && p.date == dateTime)))
                    {
                        Currency cur = db.currencies.Find(item.Attributes[0].InnerText);
                        db.currencyRates.Add(new CurrencyRate
                        {
                            date = dateTime,
                            Value = Convert.ToDecimal(item.ChildNodes[4].InnerText),
                            currency = cur
                        });
                    }
                }
                db.SaveChanges();
            }
            catch(Exception)
            {

            }            
        }

        public static void LoadCurrency(ApplicationContext db)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("http://www.cbr.ru/scripts/XML_valFull.asp");

                foreach (XmlElement item in xml.DocumentElement)
                {
                    if (db.currencies.Find(item.Attributes[0].InnerText) == null)
                    {
                        db.currencies.Add(new Currency
                        {
                            Id = item.Attributes[0].InnerText,
                            Name = item.ChildNodes[0].InnerText,
                            EngName = item.ChildNodes[1].InnerText,
                            Nominal = Convert.ToInt32(item.ChildNodes[2].InnerText),
                            ParentCode = item.ChildNodes[3].InnerText.Trim(),
                            ISO_Num_Code = item.ChildNodes[4].InnerText != "" ? Convert.ToInt32(item.ChildNodes[4].InnerText) : 0,
                            ISO_Char_Code = item.ChildNodes[5].InnerText != "" ? item.ChildNodes[5].InnerText : ""
                        });
                    }
                }
                db.SaveChanges();
            }
            catch (Exception)
            {

            }

        }
    }
}
