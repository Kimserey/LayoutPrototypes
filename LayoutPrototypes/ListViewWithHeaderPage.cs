using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class HeaderViewModel : INotifyPropertyChanged
	{
		private QuickAccess quickAccess1;

		public event PropertyChangedEventHandler PropertyChanged;

		public QuickAccess QuickAccess1
		{
			get { return quickAccess1; }
			set {
				quickAccess1 = value;
				OnPropertyChanged();
			}
		}

		void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public HeaderViewModel()
		{
			this.QuickAccess1 = new QuickAccess { Label = "Test", Color = Color.Red };
		}
	}


	public class QuickAccess
	{
		public string Label { get; set; }
		public Color Color { get; set; } 
	}

	public class StoreHeaderTemplate : Grid
	{
		public Label QuickAccess1Name { get; set; }
		public BoxView QuickAccess1Box { get; set; }

		public StoreHeaderTemplate(Action<string> goTo1)
		{
			QuickAccess1Box = new BoxView();
			QuickAccess1Name = 
				new Label { 
					FontSize = 9, 
					BackgroundColor = Color.Blue.MultiplyAlpha(0.5), 
					VerticalTextAlignment = TextAlignment.Center, 
					HorizontalTextAlignment = TextAlignment.Center 
				};
			QuickAccess1Box.GestureRecognizers.Add(new TapGestureRecognizer { 
				Command = new Command((obj) => { goTo1(QuickAccess1Name.Text); })
			});
			                                       
			this.Margin = 5;
			this.ColumnSpacing = 5;
			this.ColumnDefinitions.Add(new ColumnDefinition());
			this.ColumnDefinitions.Add(new ColumnDefinition());
			this.ColumnDefinitions.Add(new ColumnDefinition());
			this.RowDefinitions.Add(new RowDefinition());
			this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
			this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) });
			this.RowDefinitions.Add(new RowDefinition());
			this.Children.Add(new Label { Text = "Quick access (with most recent added baskets)", FontSize = 10, FontAttributes = FontAttributes.Bold }, 0, 3, 0, 1);
			this.Children.Add(QuickAccess1Box, 0, 1, 1, 3);
			this.Children.Add(QuickAccess1Name, 0, 1, 2, 3);
			this.Children.Add(new BoxView { BackgroundColor = Color.Blue }, 1, 2, 1, 3);
			this.Children.Add(new BoxView { BackgroundColor = Color.Blue }, 2, 3, 1, 3);
			this.Children.Add(new Label { Text = "All stores (alphabetical order)", FontSize = 10, FontAttributes = FontAttributes.Bold }, 0, 3, 3, 4);
		}
	}

	public class ListViewWithHeaderPage : ContentPage
	{
		public ListViewWithHeaderPage()
		{
			var list = new ListView(ListViewCachingStrategy.RecycleElement);
			list.ItemsSource = Enumerable.Range(0, 20).Select(arg => "Something " + arg).ToList();

			var vm = new HeaderViewModel();

			// Had to set the binding within the datatemplate.
			// Not sure why but the template IS NOT a ViewCell.
			// Passing a ViewCell will crash the header with an incorrect value error.
			list.HeaderTemplate = new DataTemplate(() => {
				var template = new StoreHeaderTemplate(async arg => await this.DisplayAlert("Navigate", "Go to page " + arg, "Ok"));
				//template.SetBinding(StoreHeaderTemplate.QuickAccess1Property, "QuickAccess1");
				template.QuickAccess1Box.SetBinding(BoxView.ColorProperty, "QuickAccess1.Color");
				template.QuickAccess1Name.SetBinding(Label.TextProperty, "QuickAccess1.Label");
				return template;
			});

			var button = new Button { Text = "Make blue" };
			button.Clicked += (sender, e) => {
				((HeaderViewModel)list.Header).QuickAccess1 = new QuickAccess { Color = Color.Blue, Label = "It's blue now" };
			};
			list.Footer = button;

			list.SetBinding(ListView.HeaderProperty, ".");
			this.BindingContext = vm;

			var label = 
				new Label { 
					VerticalTextAlignment = TextAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center
				};
			label.SetBinding(Label.TextProperty, "QuickAccess1.Label");

			Content = new StackLayout
			{
				Children = {
					list,
					label
				}
			};
		}
	}
}

