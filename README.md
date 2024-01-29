# convert-tokens
Resize and Convert images to transparent bordered tokens for VTTs

## Usage

`convert-tokens.exe <outputSizePixels> <inputFolderPath> <outputFolderPath> <maskFileFilePath> [<borderFilePath>]`

### Parameters

`outputSizePixels` (Required) integer from 16 to 4096 pixels; image will be resized to this square. Input images should be square, probably, or who knows what happens? make it easy.

`inputFolderPath` (Required) full path to input folder, does recursive search of the following file types:

".bmp", ".gif", ".jpe", ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".webp", ".wmf"

`outputFolderPath` (Required) path to output folder. original folder structure isn't preserved, so keep filenames unique or they will be overwritten.

todo: somebody can add option to preserve original file structure versus flatten it. I found it easier to have one big dump of tokens in a single folder.

`maskFileFilePath` (Required) mask file to use for transparency. should have center part (or whatever part you like) transparent so the artwork shows through!

use a consistent color for the mask. see the overlays folder for sample mask files in 256x256 px

`borderFilePath` (Optional) image file for border. usually a "donut" shape with outside and inside transparent, matching the mask (but should usually overlap mask by about 1px on either side to avoid ugly antialiasing and stuff

## Sample Usage

`convert-tokens.exe 256 "D:\convert-tokens\Monsters" "D:\convert-tokens\TransparentTokens" "D:\convert-tokens\overlays\transparent-mask-pink.png" "D:\convert-tokens\overlays\transparent-thick-token-ring.png"`

## Sample Overlays

These are what I used. Some of the art I was converting already had borders or didn't have super consistent edges, so I use a nice, thick border from [tokenstamp.com](https://rolladvantage.com/tokenstamp/)

- pink mask: use it with the thick border
- green mask: use it with the thin border

If you make your own overlays, the mask should be a solid color because the tool will just pick pixel 1,1 and make it transparent (feel free to fork and change this). Also, I found it helps with blending/antialiasing if the mask outer edge is approx 1px inside where you will stick the border (if you use a border).


## How to Build and use it

**Easy mode**: Download the dotnet 8 SDK from Microsoft and then use VS Code (both are free) to open the solution. It will pretty much do everything for you.

If you are new to dotnet and VS Code, take a little time to familiarize yourself. But basically open the solution in Solution Explorer and hit CTRL+F5 to build/run, or look for a Build menu. Use Terminal to find the file convert-tokens.exe and invoke it with no parameters to see the list of required parameters.

**Pro mode**: If you are a dotnet programmer you probably know other ways to build it.

`dotnet build d:\convert-tokens\convert-tokens.csproj` etc.

`God mode:` You probably already wrote your own smalltalk script or used a LLM to do this. Why are you even here?

EOF
