using deneysan_DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneysan_BLL.SearchBL
{
    public class SearchManager
    {
        public static List<Tuple<string, string>> Search(string text)
        {
            string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

            using (DeneysanContext db = new DeneysanContext())
            {
                var projects = db.Projects.Where(d=>d.Online == true).FullTextSearch(text);
                var prods = db.Product.Where(d=>d.Online == true & d.Deleted == false).FullTextSearch(text);
                var result = new List<Tuple<string, string>>();
                string route, link = string.Empty;

                foreach (var item in projects)
                {
                    if (lang.Equals("tr"))
                        route = "projeler";
                    else
                        route = "projects";
                    
                    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.ProjectId;
                    
                    result.Add(Tuple.Create(item.Name, link));   
                }

                foreach (var item in prods)
                {
                    if (lang.Equals("tr"))
                        route = "urunler";
                    else
                        route = "products";

                    deneysan_DAL.Entities.Product prod = ProductBL.ProductManager.GetProductById(item.ProductId);

                    if (prod != null)
                    {
                        link = "/" + lang + "/" + route + "/" + prod.ProductGroup.PageSlug + "/" + item.PageSlug + "/" + item.ProductId;

                        result.Add(Tuple.Create(item.Name, link));
                    }

                    
                }
                return result;
            }
        }
    }
}
