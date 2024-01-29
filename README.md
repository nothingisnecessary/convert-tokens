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


EOF
