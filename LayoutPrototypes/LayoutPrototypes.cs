using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LayoutPrototypes
{

	public class AddItemPage : ContentPage
	{
		public Layout<View> MakePriceLayout(string name, Color color) { 
			var layout = new AbsoluteLayout();
			layout.Children.Add(new Label { Text = name, VerticalTextAlignment = TextAlignment.Center }, new Rectangle(0, 0, .5, .5), AbsoluteLayoutFlags.All);
			layout.Children.Add(new Label { Text = "Waitrose", VerticalTextAlignment = TextAlignment.Center }, new Rectangle(0, 1, .5, .5), AbsoluteLayoutFlags.All);
			layout.Children.Add(new Label { Text = "$10.00", HorizontalTextAlignment = TextAlignment.End, VerticalTextAlignment = TextAlignment.Center }, new Rectangle(1, .5, .5, .5), AbsoluteLayoutFlags.All);
			return layout;
		}

		public AddItemPage()
		{
			this.Title = "Add item page";

			var layout = new Grid();
			layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
			layout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });

			var name = new Entry { Placeholder = "Name" };
			var price = new Entry { Placeholder = "Price", Keyboard = Keyboard.Numeric };
			var low = MakePriceLayout("Low", Color.Accent);
			var avg = MakePriceLayout("Average", Color.Accent);
			var high = MakePriceLayout("High", Color.Accent); 

			var sideLayout = new StackLayout { Margin = new Thickness(20) };
			sideLayout.Children.Add(name);
			sideLayout.Children.Add(price);
			sideLayout.Children.Add(low);
			sideLayout.Children.Add(new Frame { Padding = new Thickness(5), Content = avg, BackgroundColor = Color.Accent });
			sideLayout.Children.Add(high);
			layout.Children.Add(sideLayout, 1, 0);

			var list = new ListView
			{
				ItemsSource = new List<String> {
					"Baking powder",
					"Budweiser", 
					"Colgate", 
					"Tissue", 
					"Double cream", 
					"Beef", 
					"Chicken breast"
				},
				ItemTemplate =
						new DataTemplate(() =>
						{
							var template = new TextCell();
							template.SetBinding(TextCell.TextProperty, ".");
							return template;
						}),
				BackgroundColor = Color.White
			};
			layout.Children.Add(list, 0, 0);

			this.Content = layout;
		}
	}

	public class Item { 
		public string Name
		{
			get;
			set;
		}

		public decimal Price
		{
			get;
			set;
		}	
	}

	public class BasketPage : ContentPage
	{
		public BasketPage()
		{
			this.Title = "Basket 1";

			var layout = new AbsoluteLayout { Margin = new Thickness(0) };
			var currentTotal = new StackLayout
			{
				Padding = new Thickness { Top = 20, Bottom = 20 },
				BackgroundColor = Color.Accent
			};
			var label = new Label { Text = "Current total", Style = Device.Styles.TitleStyle, HorizontalTextAlignment = TextAlignment.Center };
			var total = new Label { Text = "$35.10", Style = Device.Styles.SubtitleStyle, HorizontalTextAlignment = TextAlignment.Center };
			currentTotal.Children.Add(label);
			currentTotal.Children.Add(total);
			layout.Children.Add(currentTotal, new Rectangle(0, 0, 1, .2), AbsoluteLayoutFlags.All);

			var listLayout =
				new ListView
				{
					ItemsSource = new List<Item> {
						new Item { Name = "Baking powder", Price = 1.5m },
						new Item { Name = "Budweiser", Price = 1.5m },
						new Item { Name = "Colgate", Price = 1.5m },
						new Item { Name = "Tissue", Price = 1.5m },
						new Item { Name = "Double cream", Price = 1.5m },
						new Item { Name = "Beef", Price = 1.5m },
						new Item { Name = "Chicken breast", Price = 1.5m },
						new Item { Name = "Baking powder", Price = 1.5m }
					},
					ItemTemplate =
						new DataTemplate(() =>
						{
							var template = new AbsoluteLayout { Padding = new Thickness { Left = 10, Right = 10 } };
							var name = new Label { HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center };
							var price = new Label { HorizontalTextAlignment = TextAlignment.End, VerticalTextAlignment = TextAlignment.Center };
							template.Children.Add(name, new Rectangle(0, 0, .5, 1), AbsoluteLayoutFlags.All);
							template.Children.Add(price, new Rectangle(1, 0, .5, 1), AbsoluteLayoutFlags.All);
							name.SetBinding(Label.TextProperty, "Name");
							price.SetBinding(Label.TextProperty, "Price", stringFormat: "{0:C2}");
							return new ViewCell { View = template };
						})
				};
			var scroll = new ScrollView { Content = listLayout };

			layout.Children.Add(scroll, new Rectangle(0, 1, 1, .8), AbsoluteLayoutFlags.All);


			this.ToolbarItems.Add(
				new ToolbarItem(
					"Add new item",
					"",
					async () => {
						await this.Navigation.PushAsync(new AddItemPage());
					})
			);

			this.Content = layout;
		}

	}

	public class MasterItem {
		
		public MasterItem(string title, string icon, Type viewType) {
			this.Title = title;
			this.Icon = icon;
			this.ViewType = viewType;
		}

		public string Title
		{
			get;
			set;
		}

		public string Icon
		{
			get;
			set;
		}

		public Type ViewType
		{
			get;
			set;
		}
	}

	public class MasterPage : ContentPage {
		public MasterPage(Action<string> go) {

			var list = new ListView
			{
				ItemsSource = new List<MasterItem> {
					new MasterItem("Overview", "ic_home_black_24dp.png", typeof(BasketPage)),
					new MasterItem("Baskets", "ic_shopping_basket_black_24dp.png", typeof(BasketPage)), 
					new MasterItem("Item lookup", "ic_search_black_24dp.png", typeof(BasketPage)),
					new MasterItem("About", "ic_search_black_24dp.png", typeof(BasketPage))
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None,
				ItemTemplate = new DataTemplate(() => {
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

			list.ItemTapped += (sender, e) => {
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

	public class MainPage : MasterDetailPage {

		public MainPage()
		{
			this.Title = "Basket";

			this.Detail = new NavigationPage(new BasketPage());
			this.Master = new MasterPage(x => {
				this.IsPresented = false;
			});
		}
	}

	public class App : Application
	{
		public App()
		{
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

