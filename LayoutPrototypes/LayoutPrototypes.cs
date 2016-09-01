using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace LayoutPrototypes
{

	public class AddItemPage : ContentPage
	{
		public View MakePriceLayout(string name, Color textColor, Color color) {
			var layout = new AbsoluteLayout { Padding = new Thickness(5), BackgroundColor = color };
			layout.Children.Add(new Label { Text = name, TextColor = textColor, VerticalTextAlignment = TextAlignment.Center }, new Rectangle(0, 0, .5, .5), AbsoluteLayoutFlags.All);
			layout.Children.Add(new Label { Text = "Waitrose", TextColor = textColor, VerticalTextAlignment = TextAlignment.Center }, new Rectangle(0, 1, .5, .5), AbsoluteLayoutFlags.All);
			layout.Children.Add(new Label { Text = "$10.00", TextColor = textColor, HorizontalTextAlignment = TextAlignment.End, VerticalTextAlignment = TextAlignment.Center }, new Rectangle(1, .5, .5, .5), AbsoluteLayoutFlags.All);
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
			var low = MakePriceLayout("Low", Color.White, Color.Purple);
			var avg = MakePriceLayout("Average", Color.White, Color.Teal);
			var high = MakePriceLayout("High", Color.White, Color.Red); 

			var sideLayout = new StackLayout { Margin = new Thickness(5) };
			sideLayout.Children.Add(name);
			sideLayout.Children.Add(price);
			sideLayout.Children.Add(low);
			sideLayout.Children.Add(avg);
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
							template.TextColor = Color.Gray;
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
		public string Name { get; set; }
		public decimal Price { get; set; }	
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

	public class PhotoPage : ContentPage {
		public PhotoPage() {

			var image = new Image();

			var buttonTakePhoto = new Button { Text = "Take a photo" };
			buttonTakePhoto.Clicked += async (sender, e) => 
			{
				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
				{
					Directory = "Sample",
					Name = "test.jpg"
				});

				if (file == null)
					return;

				await DisplayAlert("Path", file.AlbumPath + "\n" + file.Path, "OK");
				image.Source = file.Path;
			};


			var buttonPick = new Button { Text = "Pick a photo" };
			buttonPick.Clicked += async (sender, args) =>
			{
				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
					return;
				}
				var file = await CrossMedia.Current.PickPhotoAsync();


				if (file == null)
					return;

				image.Source = file.Path;

			};

			this.Content = new StackLayout { Children = { image, buttonTakePhoto, buttonPick } };
		}
	}
}

