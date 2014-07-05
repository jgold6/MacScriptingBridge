using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ScriptingBridge;
using MonoMac.ObjCRuntime;

namespace MacScriptingBridge
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{

		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		// Call to load from the XIB/NIB file
		public MainWindowController() : base("MainWindow")
		{
			Initialize();
		}
		// Shared initialization code
		void Initialize()
		{

		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			// ScriptingBridge Tests
			//
			// FinderApplication *finder = [SBApplication applicationWithBundleIdentifier:@"com.apple.finder"];
			SBApplication finder=SBApplication.FromBundleIdentifier("com.apple.finder");
			// SBElementArray *disks = [finder disks];
			IntPtr disks = Messaging.IntPtr_objc_msgSend (finder.Handle, Selector.GetHandle ("disks"));
			//FinderDisk *disk = [disks objectAtIndex:0];
			IntPtr disk = Messaging.IntPtr_objc_msgSend_int (disks, Selector.GetHandle ("objectAtIndex:"), 0);
			// NSString *name = [disk name]; // lazy evaluation occurs
			IntPtr name = Messaging.IntPtr_objc_msgSend (disk, Selector.GetHandle ("name"));
			string strname = new NSString (name);
			// NSLog(@"Name of first disk is %@", strname);
			Console.WriteLine ("Name of first disk is {0}", strname);

			// iTunes
			SBApplication iTunes=SBApplication.FromBundleIdentifier("com.apple.iTunes");
			// Get Volume
			IntPtr ptrVol = Messaging.IntPtr_objc_msgSend(iTunes.Handle, Selector.GetHandle ("soundVolume"));
			int vol = (int)ptrVol;
			// Show Volume to console
			Console.WriteLine("Volume: {0}", vol);
			lblVolume.StringValue = String.Format("Volume: {0}", vol); // in app label
			sliderVolume.TickMarksCount = 101; // set the number of "stops"
			sliderVolume.IntValue = vol; // Set the slider value based on the volume
			sliderVolume.AllowsTickMarkValuesOnly = true; // Allow only integral numbers

			// Handle slider dragging (must check "continuous" in xib)
			sliderVolume.Activated += (object sender, EventArgs e) => {
				var pos = sliderVolume.IntValue; // Get slider value
				Console.WriteLine("SetVol = " + pos); //Show volume in console
				Messaging.IntPtr_objc_msgSend_int (iTunes.Handle, Selector.GetHandle ("setSoundVolume:"), pos); // Set volume in iTunes
				lblVolume.StringValue = String.Format("Volume: {0}", pos); // show volume in label
			};

			// Handle button clicks
			btnPrevTrack.Activated += (object sender, EventArgs e) => {
				Messaging.IntPtr_objc_msgSend(iTunes.Handle, Selector.GetHandle("previousTrack"));
				btnWhatsPlaying.PerformClick(this);
			};

			btnNextTrack.Activated += (object sender, EventArgs e) => {
				Messaging.IntPtr_objc_msgSend(iTunes.Handle, Selector.GetHandle("nextTrack"));
				btnWhatsPlaying.PerformClick(this);
			};

			btnWhatsPlaying.Activated += (object sender, EventArgs e) => {
				// get pointer to current track object
				IntPtr ptr = Messaging.IntPtr_objc_msgSend(iTunes.Handle, Selector.GetHandle ("currentTrack")); 
				IntPtr ptrTitle = Messaging.IntPtr_objc_msgSend(ptr, Selector.GetHandle("name")); // use pointer to track object to get song name
				IntPtr ptrArtist = Messaging.IntPtr_objc_msgSend(ptr, Selector.GetHandle("artist")); // artist
				IntPtr ptrAlbum = Messaging.IntPtr_objc_msgSend(ptr, Selector.GetHandle("album")); // and album
				string song = new NSString(ptrTitle); // convert pointers to strings
				string artist = new NSString(ptrArtist);
				string album = new NSString(ptrAlbum);
				Console.WriteLine("iTunes Song Playing: Artist: {0}, Album: {1}, Title: {2}", artist,album, song); // show in console
				lblArtist.StringValue = String.Format("Artist: {0}", artist); // Show in app labels
				lblAlbum.StringValue = String.Format("Album: {0}", album);
				lblSong.StringValue = String.Format("Song: {0}", song);
			};
		}
		#endregion

		//strongly typed window accessor
		public new MainWindow Window
		{
			get
			{
				return (MainWindow)base.Window;
			}
		}
	}
}

