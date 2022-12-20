using LINQSamples.RepositoryClasses;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LINQSamples
{
  public class SamplesViewModel
  {
    
    public SamplesViewModel()
    {
      // Load all Product Data
      Products = ProductRepository.GetAll();
    }



    public bool UseQuerySyntax { get; set; }
    public List<Product> Products { get; set; }
    public string ResultText { get; set; }
       public void WhereExtensionMethod()
        {
            string search = "Red";
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                            select prod)
                            .ByColor(search).ToList();
            }
            else
            {
                Products = Products.ByColor(search).ToList();
            }
            ResultText = $"Total Products:{Products.Count}";
        }
        public void WhereTwoFiels()
        {
            string search = "L";
            decimal cost = 100;
            if(UseQuerySyntax)
            {
                Products = (from prod in Products where prod.Name.StartsWith(search)
                           && prod.StandardCost > cost select prod).ToList();
            }
            else
            {
                Products = Products
                    .Where(prod => prod.Name.StartsWith(search)&&
                    prod.StandardCost > cost).ToList();
            }
            ResultText = $"Total Products: {Products.Count}";
        }
    public void WhereExpression()
        {
            string search = "L";
            if(UseQuerySyntax)
            {
                Products = (from prod in Products where prod.Name.StartsWith(search)
                            select prod).ToList();
            }
            else
            {
                Products = Products
                    .Where(prod => prod.Name.StartsWith(search)).ToList();
            }
            ResultText = $"Total Products :{Products.Count}";
        }
    public void GetAllLooping()
    {
      List<Product> list = new List<Product>();

     
      ResultText = $"Total Products: {list.Count}";
    }
  
    public void GetAll()
    {
      List<Product> list = new List<Product>();

      if (UseQuerySyntax) {
               list = (from prod in Products select prod).ToList();
        
      }
      else {
                list = Products.Select(prod => prod).ToList();        
      }

      ResultText = $"Total Products: {list.Count}";
    }
   
    public void GetSingleColumn()
    {
      StringBuilder sb = new StringBuilder(1024);
      List<string> list = new List<string>();

      if (UseQuerySyntax) {
                list.AddRange(from prod in Products select prod.Name);         

      }
      else {
                list.AddRange(Products.Select(prod => prod.Name)); 
        
      }

      foreach (string item in list) {
        sb.AppendLine(item);
      }

      ResultText = $"Total Products: {list.Count}" + Environment.NewLine + sb.ToString();
      Products.Clear();
    }

    public void GetSpecificColumns()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            select new Product
                            {
                                ProductID = prod.ProductID,
                                Name = prod.Name,
                                Size = prod.Size,
                            }).ToList();       
      }
      else {
              Products = Products.Select(prod => new Product
              {
                  ProductID = prod.ProductID,
                  Name = prod.Name,
                  Size = prod.Size,
              }).ToList() ;
       
      }

      ResultText = $"Total Products: {Products.Count}";
    }


  
    public void AnonymousClass()
    {
      StringBuilder sb = new StringBuilder(2048);

      if (UseQuerySyntax) {
                var products = (from prod in Products
                                select new
                                {
                                    Identifier = prod.ProductID,
                                    ProductName = prod.Name,
                                    ProductSize = prod.Size
                                });
        
        // Loop through anonymous class
        foreach (var prod in products) {
          sb.AppendLine($"Product ID: {prod.Identifier}");
          sb.AppendLine($"   Product Name: {prod.ProductName}");
          sb.AppendLine($"   Product Size: {prod.ProductSize}");
        }
      }
      else {
                // Method Syntax
                var products = Products.Select(prod => new
                {
                    Identifier = prod.ProductID,
                    ProductName = prod.Name,
                    ProductSize = prod.Size,
                }); 
        // Loop through anonymous class
        foreach (var prod in products) {
          sb.AppendLine($"Product ID: {prod.Identifier}");
          sb.AppendLine($"   Product Name: {prod.ProductName}");
          sb.AppendLine($"   Product Size: {prod.ProductSize}");
        }
      }

      ResultText = sb.ToString();
      Products.Clear();
    }



    /// <summary>
    /// Order products by Name
    /// </summary>
    public void OrderBy()
    {
      if (UseQuerySyntax) {
            Products = (from prod in Products
                        orderby prod.Name
                        select prod).ToList();
      }
      else {
        // Method Syntax
        Products = Products.OrderBy(prod => prod.Name).ToList();
      }

      ResultText = $"Total Products: {Products.Count}";
    }

    public void OrderByDescending()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            orderby prod.Name descending
                            select prod).ToList();

            }
      else {
                // Method Syntax
                Products = Products.OrderByDescending(prod => prod.Name).ToList();

            }

      ResultText = $"Total Products: {Products.Count}";
    }
 
    public void OrderByTwoFields()
    {
      if (UseQuerySyntax) {
                Products = (from prod in Products
                            orderby prod.Color descending, prod.Name
                            select prod).ToList();
      }
      else {
        // Method Syntax
        Products = Products.OrderByDescending(prod => prod.Color)
        .ThenBy(prod => prod.Name).ToList();

      }

      ResultText = $"Total Products: {Products.Count}";
    }
    
  }
}
