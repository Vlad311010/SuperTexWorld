using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPProject.Models.Home;


namespace ASPProject.Helpers
{
	public static class UserExtensions
    {
		static ShopEntities context = new ShopEntities();
		public static string GetUserEmail(this HtmlHelper helper, int userId)
        {
			User user = context.Users.Find(userId);

			return user.Email;
		}
	}
	public static class ItemExtensions
	{
		static ShopEntities context = new ShopEntities();
		public static IHtmlString ShowItemName(this HtmlHelper helper, int itemId)
        {
			Item item = context.Items.Find(itemId);
			string html =
					"{0}";
			html = string.Format(html, item.ItemName);
			return new MvcHtmlString(html);
		}

		public static IHtmlString ShowItemPrice(this HtmlHelper helper, int itemId)
		{
			Item item = context.Items.Find(itemId);
			string html =
					"{0}";
			html = string.Format(html, item.Price);
			return new MvcHtmlString(html);
		}

		public static double GetItemPrice(this HtmlHelper helper, int itemId)
		{
			Item item = context.Items.Find(itemId);
			return item.Price;
		}

		public static IHtmlString ShowItem(this HtmlHelper helper, Item item)
		{
			string html =
					"<p data-toggle='modal' data-target='#myModal1' class='offer-img'>" +
						"<img src = 'Content/DataImage/{0}' class='img-responsive item-img' alt=''>" +
					"</p>" +
					"<div class='mid-1'>" +
						"<div class='women'>" +
							"<h6>{1}</h6>" +
						"</div>" +
						"<div class='mid-2'>" +
							"<p ><em class='item_price'>${2}</em></p>" +
							"<div class='block'>" +
								"<div class='starbox small ghosting'> </div>" +
							"</div>" +
							"<div class='clearfix'></div>" +
						"</div>" +
					"</div>";
			html = string.Format(html, item.ImagePath, item.ItemName, item.Price, item.Id);

			return new MvcHtmlString(html);
        }
    }
}