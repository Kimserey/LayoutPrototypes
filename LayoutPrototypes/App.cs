using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class MasterItem
	{

		public MasterItem(string title, string icon, Type viewType)
		{
			this.Title = title;
			this.Icon = icon;
			this.ViewType = viewType;
		}

		public string Title { get; set; }
		public string Icon { get; set; }
		public Type ViewType { get; set; }
	}

	public class MasterPage : ContentPage
	{
		public MasterPage(Action<string> go)
		{

			var list = new ListView
			{
				ItemsSource = new List<MasterItem> {
					new MasterItem("Overview", "ic_home_black_24dp.png", typeof(BasketPage)),
					new MasterItem("Baskets", "ic_shopping_basket_black_24dp.png", typeof(BasketPage)),
					new MasterItem("Item lookup", "ic_search_black_24dp.png", typeof(BasketPage)),
					new MasterItem("About", "ic_info_outline_black_24dp.png", typeof(BasketPage))
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None,
				ItemTemplate = new DataTemplate(() =>
				{
					var cell = new AbsoluteLayout();
					var image = new Image();
					var label = new Label { VerticalTextAlignment = TextAlignment.Center };
					cell.Children.Add(image, new Rectangle(0, 0, .2, 1), AbsoluteLayoutFlags.All);
					cell.Children.Add(label, new Rectangle(1, 0, .8, 1), AbsoluteLayoutFlags.All);
					label.SetBinding(Label.TextProperty, "Title");
					image.SetBinding(Image.SourceProperty, "Icon");
					return new ViewCell { View = cell };
				})
			};

			list.ItemTapped += (sender, e) =>
			{
				var selectedItem = e.Item as string;
				go(selectedItem);
			};

			var layout = new StackLayout();
			var imageLayout = new AbsoluteLayout { BackgroundColor = Color.Purple };
			var brand = new Label { Text = "My brand", HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Color.White };
			imageLayout.Children.Add(brand, new Rectangle(0.5, 0.5, 100, 100), AbsoluteLayoutFlags.PositionProportional);
			layout.Children.Add(imageLayout);
			layout.Children.Add(list);
			this.Content = layout;
			this.Title = "Master page";
		}
	}

	public class MainPage : MasterDetailPage
	{

		public MainPage()
		{
			this.Title = "Basket";

			this.Detail = new NavigationPage(new PhotoPage());
			this.Master = new MasterPage(x =>
			{
				this.IsPresented = false;
			});
		}
	}

	public class App : Application
	{
		public App()
		{
			MainPage = new TabPage();
		}
	}
}


