using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LayoutPrototypes
{

	public class ItemPage : ContentPage {
		public ItemPage()
		{
			this.Title = "Items";
				
			var layout = new AbsoluteLayout();
			var currentTotal = new StackLayout {
				Padding = new Thickness { Top = 20, Bottom = 20 }
			};
			var label = new Label { Text = "Current total", Style = Device.Styles.TitleStyle, HorizontalTextAlignment = TextAlignment.Center };
			var total = new Label { Text = "$35.10", Style = Device.Styles.SubtitleStyle, HorizontalTextAlignment = TextAlignment.Center };
			currentTotal.Children.Add(label);
			currentTotal.Children.Add(total);
			layout.Children.Add(currentTotal, new Rectangle(0, 0, 1, .2), AbsoluteLayoutFlags.All);

			var listLayout = 
				new ListView {
					ItemsSource = new List<String> { "Hello", "World", "Hi", "Bonjour", "Holla", "Salut", "Yo", "Monday", "Thursday", "Hello", "World", "Hi", "Bonjour", "Holla", "Salut", "Yo", "Monday", "Thursday" },
					ItemTemplate =
						new DataTemplate(() => {
							var template = new TextCell();
							template.SetBinding(TextCell.TextProperty, ".");
							return template;
						})
				};
			var scroll = new ScrollView { Content = listLayout };

			layout.Children.Add(scroll, new Rectangle(0, 1, 1, .8), AbsoluteLayoutFlags.All);
			this.Content = layout;
		}

	}

	public class App : Application
	{
		public App()
		{
			var content = new ItemPage();
			MainPage = new NavigationPage(content);
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

