using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp;

namespace AppHokeyDemo
{
	[Activity(Label = "AppHokeyDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			InitializeHockeyApp();
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.MyButton);

			button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
			var feedbackButton = FindViewById<Button>(Resource.Id.feedback_button);

			feedbackButton.Click += delegate
			{
				FeedbackManager.ShowFeedbackActivity(ApplicationContext);
			};

			FindViewById<Button>(Resource.Id.buttonCauseCrash).Click += delegate
			{
				// Throw a deliberate sample crash
				throw new HockeyAppSampleException("You intentionally caused a crash!");
			};
		}
		public class HockeyAppSampleException : System.Exception
		{
			public HockeyAppSampleException(string msg)
				: base(msg)
			{
			}
		}
		
		void InitializeHockeyApp()
		{
			HockeyApp.CrashManager.Register(this, "App ID");
			HockeyApp.Metrics.MetricsManager.Register(this, Application, "App ID");
			HockeyApp.FeedbackManager.Register(this, "App ID");
			HockeyApp.Metrics.MetricsManager.EnableUserMetrics();
			
		}
	}
}

