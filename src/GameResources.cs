using System.Collections.Generic;
using SwinGameSDK;
/// <summary>
/// Game resource file are loaded from this class
/// </summary>
public static class GameResources
{
    /// <summary>
    /// Declaration of dictionary for image, font, sound, and music
    /// </summary>
    private static Dictionary<string, Bitmap> _Images = new Dictionary<string, Bitmap>();
    private static Dictionary<string, Font> _Fonts = new Dictionary<string, Font>();
    private static Dictionary<string, SoundEffect> _Sounds = new Dictionary<string, SoundEffect>();
    private static Dictionary<string, Music> _Music = new Dictionary<string, Music>();

    /// <summary>
    /// Declaration of variables
    /// </summary>
    private static Bitmap _Background;
    private static Bitmap _Animation;
    private static Bitmap _LoaderFull;
    private static Bitmap _LoaderEmpty;
    private static Font _LoadingFont;
    private static SoundEffect _StartSound;

    /// <summary>
    /// Setup the font to be load
    /// </summary>
    private static void LoadFonts()
	{
		NewFont("ArialLarge", "arial.ttf", 80);
		NewFont("Courier", "cour.ttf", 14);
		NewFont("CourierSmall", "cour.ttf", 8);
		NewFont("Menu", "ffaccess.ttf", 8);
	}

    /// <summary>
    /// Setup the image to be load
    /// </summary>
    private static void LoadImages()
	{
		//Backgrounds
		NewImage("Menu", "main_page.jpg");
		NewImage("Discovery", "discover.jpg");
		NewImage("Deploy", "deploy.jpg");

		//Deployment
		NewImage("LeftRightButton", "deploy_dir_button_horiz.png");
		NewImage("UpDownButton", "deploy_dir_button_vert.png");
		NewImage("SelectedShip", "deploy_button_hl.png");
		NewImage("PlayButton", "deploy_play_button.png");
		NewImage("RandomButton", "deploy_randomize_button.png");

		//Ships
		int i = 0;
		for (i = 1; i <= 5; i++) {
			NewImage("ShipLR" + i, "ship_deploy_horiz_" + i + ".png");
			NewImage("ShipUD" + i, "ship_deploy_vert_" + i + ".png");
		}

		//Explosions
		NewImage("Explosion", "explosion.png");
		NewImage("Splash", "splash.png");

	}

    /// <summary>
    /// Setup the sound to be load
    /// </summary>
    private static void LoadSounds()
	{
		NewSound("Error", "error.wav");
		NewSound("Hit", "hit.wav");
		NewSound("Sink", "sink.wav");
		NewSound("Siren", "siren.wav");
		NewSound("Miss", "watershot.wav");
		NewSound("Winner", "winner.wav");
		NewSound("Lose", "lose.wav");
	}

    /// <summary>
    /// Setup the music to be load
    /// </summary>
    private static void LoadMusic()
	{
		NewMusic("Background", "alienspaceship.wav");
	}

	/// <summary>
	/// Gets a Font Loaded in the Resources
	/// </summary>
	/// <param name="font">Name of Font</param>
	/// <returns>The Font Loaded with this Name</returns>
	public static Font GameFont(string font)
	{
		return _Fonts[font];
	}

	/// <summary>
	/// Gets an Image loaded in the Resources
	/// </summary>
	/// <param name="image">Name of image</param>
	/// <returns>The image loaded with this name</returns>
	public static Bitmap GameImage(string image)
	{
		return _Images[image];
	}

	/// <summary>
	/// Gets an sound loaded in the Resources
	/// </summary>
	/// <param name="sound">Name of sound</param>
	/// <returns>The sound with this name</returns>
	public static SoundEffect GameSound(string sound)
	{
		return _Sounds[sound];
	}

	/// <summary>
	/// Gets the music loaded in the Resources
	/// </summary>
	/// <param name="music">Name of music</param>
	/// <returns>The music with this name</returns>
	public static Music GameMusic(string music)
	{
		return _Music[music];
	}

	/// <summary>
	/// The Resources Class stores all of the Games Media Resources, such as Images, Fonts
	/// Sounds, Music.
	/// </summary>
	public static void LoadResources()
	{
		int width = 0;
		int height = 0;

		width = SwinGame.ScreenWidth();
		height = SwinGame.ScreenHeight();

		SwinGame.ChangeScreenSize(800, 600);

		ShowLoadingScreen();

		ShowMessage("Loading fonts...", 0);
		LoadFonts();
		SwinGame.Delay(100);

		ShowMessage("Loading images...", 1);
		LoadImages();
		SwinGame.Delay(100);

		ShowMessage("Loading sounds...", 2);
		LoadSounds();
		SwinGame.Delay(100);

		ShowMessage("Loading music...", 3);
		LoadMusic();
		SwinGame.Delay(100);

		SwinGame.Delay(100);
		ShowMessage("Game loaded...", 5);
		SwinGame.Delay(100);
		EndLoadingScreen(width, height);
	}

    /// <summary>
    /// Show the loading screen
    /// </summary>
	private static void ShowLoadingScreen()
	{
		_Background = SwinGame.LoadBitmap(SwinGame.PathToResource("SplashBack.png", ResourceKind.BitmapResource));
		SwinGame.DrawBitmap(_Background, 0, 0);
		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();

		_Animation = SwinGame.LoadBitmap(SwinGame.PathToResource("SwinGameAni.jpg", ResourceKind.BitmapResource));
		_LoadingFont = SwinGame.LoadFont(SwinGame.PathToResource("arial.ttf", ResourceKind.FontResource), 12);
		_StartSound = Audio.LoadSoundEffect(SwinGame.PathToResource("SwinGameStart.ogg", ResourceKind.SoundResource));

		_LoaderFull = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_full.png", ResourceKind.BitmapResource));
		_LoaderEmpty = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_empty.png", ResourceKind.BitmapResource));

		PlaySwinGameIntro();
	}

    /// <summary>
    /// Play the game intro
    /// </summary>
	private static void PlaySwinGameIntro()
	{
		const int ANI_X = 143;
		const int ANI_Y = 134;
		const int ANI_W = 546;
		const int ANI_H = 327;
		const int ANI_V_CELL_COUNT = 6;
		const int ANI_CELL_COUNT = 11;

		Audio.PlaySoundEffect(_StartSound);
		SwinGame.Delay(200);

		int i = 0;
		for (i = 0; i <= ANI_CELL_COUNT - 1; i++) {
			SwinGame.DrawBitmap(_Background, 0, 0);
			SwinGame.DrawBitmapPart(_Animation, (i / ANI_V_CELL_COUNT) * ANI_W, (i % ANI_V_CELL_COUNT) * ANI_H, ANI_W, ANI_H, ANI_X, ANI_Y);
			SwinGame.Delay(20);
			SwinGame.RefreshScreen();
			SwinGame.ProcessEvents();
		}

		SwinGame.Delay(1500);

	}

    /// <summary>
    /// Show message of the program
    /// </summary>
    /// <param name="message"></param>
    /// <param name="number"></param>
	private static void ShowMessage(string message, int number)
	{
		const int TX = 310;
		const int TY = 493;
		const int TW = 200;
		const int TH = 25;
		const int STEPS = 5;
		const int BG_X = 279;
		const int BG_Y = 453;

		int fullW = 0;

		fullW = 260 * number / STEPS;
		SwinGame.DrawBitmap(_LoaderEmpty, BG_X, BG_Y);
		SwinGame.DrawBitmapPart(_LoaderFull, 0, 0, fullW, 66, BG_X, BG_Y);

		SwinGame.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, TX, TY, TW, TH);

		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();
	}

    /// <summary>
    /// Handle the end loading screen
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
	private static void EndLoadingScreen(int width, int height)
	{
		SwinGame.ProcessEvents();
		SwinGame.Delay(500);
		SwinGame.ClearScreen();
		SwinGame.RefreshScreen();
		SwinGame.FreeFont(_LoadingFont);
		SwinGame.FreeBitmap(_Background);
		SwinGame.FreeBitmap(_Animation);
		SwinGame.FreeBitmap(_LoaderEmpty);
		SwinGame.FreeBitmap(_LoaderFull);
		Audio.FreeSoundEffect(_StartSound);
		SwinGame.ChangeScreenSize(width, height);
	}

    /// <summary>
    /// Create font and add to _Fonts
    /// </summary>
    /// <param name="fontName"></param>
    /// <param name="filename"></param>
    /// <param name="size"></param>
	private static void NewFont(string fontName, string filename, int size)
	{
		_Fonts.Add(fontName, SwinGame.LoadFont(SwinGame.PathToResource(filename, ResourceKind.FontResource), size));
	}

    /// <summary>
    /// Create image and add to _Images
    /// </summary>
    /// <param name="imageName"></param>
    /// <param name="filename"></param>
	private static void NewImage(string imageName, string filename)
	{
		_Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(filename, ResourceKind.BitmapResource)));
	}

    /// <summary>
    /// Create transparent image and add to _Images
    /// </summary>
    /// <param name="imageName"></param>
    /// <param name="fileName"></param>
    /// <param name="transColor"></param>
	private static void NewTransparentColorImage(string imageName, string fileName, Color transColor)
	{
		_Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(fileName, ResourceKind.BitmapResource), true, transColor));
	}

	private static void NewTransparentColourImage(string imageName, string fileName, Color transColor)
	{
		NewTransparentColorImage(imageName, fileName, transColor);
	}

    /// <summary>
    /// Create sound and add to _Sounds
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="filename"></param>
	private static void NewSound(string soundName, string filename)
	{
		_Sounds.Add(soundName, Audio.LoadSoundEffect(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}

    /// <summary>
    /// Create music and add to _Music
    /// </summary>
    /// <param name="musicName"></param>
    /// <param name="filename"></param>
	private static void NewMusic(string musicName, string filename)
	{
		_Music.Add(musicName, Audio.LoadMusic(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}

    /// <summary>
    /// Empty content in _Fonts
    /// </summary>
	private static void FreeFonts()
	{
		foreach (Font obj in _Fonts.Values) {
			SwinGame.FreeFont(obj);
		}
	}

    /// <summary>
    /// Empty content in _Images
    /// </summary>
	private static void FreeImages()
	{
		foreach (Bitmap obj in _Images.Values) {
			SwinGame.FreeBitmap(obj);
		}
	}

    /// <summary>
    /// Empty content in _Sounds
    /// </summary>
	private static void FreeSounds()
	{
		foreach (SoundEffect obj in _Sounds.Values) {
			Audio.FreeSoundEffect(obj);
		}
	}

    /// <summary>
    /// Empty content in _Music
    /// </summary>
	private static void FreeMusic()
	{
		foreach (Music obj in _Music.Values) {
			Audio.FreeMusic(obj);
		}
	}

    /// <summary>
    /// Initiate every method that was to empty the content in dictionary
    /// </summary>
	public static void FreeResources()
	{
		FreeFonts();
		FreeImages();
		FreeMusic();
		FreeSounds();
		SwinGame.ProcessEvents();
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
