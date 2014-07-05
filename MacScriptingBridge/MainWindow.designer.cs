// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace MacScriptingBridge
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSButton btnNextTrack { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton btnPrevTrack { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton btnWhatsPlaying { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblAlbum { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblArtist { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblSong { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblVolume { get; set; }

		[Outlet]
		MonoMac.AppKit.NSSlider sliderVolume { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnPrevTrack != null) {
				btnPrevTrack.Dispose ();
				btnPrevTrack = null;
			}

			if (btnNextTrack != null) {
				btnNextTrack.Dispose ();
				btnNextTrack = null;
			}

			if (btnWhatsPlaying != null) {
				btnWhatsPlaying.Dispose ();
				btnWhatsPlaying = null;
			}

			if (lblAlbum != null) {
				lblAlbum.Dispose ();
				lblAlbum = null;
			}

			if (lblArtist != null) {
				lblArtist.Dispose ();
				lblArtist = null;
			}

			if (lblSong != null) {
				lblSong.Dispose ();
				lblSong = null;
			}

			if (lblVolume != null) {
				lblVolume.Dispose ();
				lblVolume = null;
			}

			if (sliderVolume != null) {
				sliderVolume.Dispose ();
				sliderVolume = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
