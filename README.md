# convert-tokens
Batch resize and convert images to transparent bordered tokens for VTTs

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

Download the dotnet 8 SDK from Microsoft. Compile the application by opening the solution in an IDE like Visual Studio or VS Code, or compile via your console:

`dotnet build convert-tokens.csproj`

For examples see:
https://learn.microsoft.com/en-us/training/paths/build-dotnet-applications-csharp/
https://code.visualstudio.com/docs/csharp/get-started

Build the executable `convert-tokens.exe` and invoke it without any parameters to see the list of required parameters, or use the parameters listed above.

## Sample Output from ShadowDarkToken Project

The ShadowDarkTokenProject folder shows an example set of batch-processed output, by using AI generated tokens from ShadowDarkToken Project which is curated by WoolyBeard over at the official The AL server. Thanks to WoolyBeard and the ShadowDarkToken Project contributors.

EOF
