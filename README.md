# Quad64 v0.2 (Beta build 5) [Dead]
An open-source SM64 level editor written in C# 4.0, and uses Windows Forms and OpenTK.

<a href="https://i.imgur.com/NeBlYO2.png"><img src="https://i.imgur.com/NeBlYO2.png"/></a>

## This project is no longer offically being worked on.

I do not have any kind of motivation to work on this project anymore, so this branch will no longer recieve any updates. The source code will still always be avaliable and open source for anyone to look at and use for any reason. 

If you want an alternative to Quad64, then I'd recommend using Pilzinsel64's SM64 ROM Manager. It is an all-in-one tool that has both a level importer and an object editor. You can find a thread about it here on Hack64: https://hack64.net/Thread-SM64-ROM-Manager-Brand-new-all-in-one-tool-for-SM64-Hacking-Public-Beta

## Features

The main focus of the tool is to be like Toads Tool 64, but with better ROM compatibility. 
* Both extended ROMs and vanilla (8MB) ROMs that have uncompressed data can be modified.
* You can modify any of the 4 major regional versions of SM64, which includes: US, Europe, Japan, and Japan (Shindou edition)
* You can load and save ROM files as big endian(.z64), middle endian(.v64), or little endian(.n64)
* Supports most of the N64 texture formats: RGBA16, RGBA32, IA16, IA8, IA4, I8, and I4. (CI textures will be interpreted as grayscale)

Note: Macro & Special objects are not editable with the vanilla 8MB ROM, since they are MIO0 compressed. 
You can use <a href="https://github.com/queueRAM/sm64tools/releases/tag/sm64extendv0.3.1">queueRAM's sm64extend tool</a> to extend any vanilla ROM file, which will let you edit these objects.


# Required Libraries

## OpenTK
Required for the 3D Viewer window.

https://www.nuget.org/packages/OpenTK/
<br/>
https://www.nuget.org/packages/OpenTK.GLControl/

## JSON.NET
Required for reading/writing JSON files.

http://www.newtonsoft.com/json

# License (MIT)

Copyright (c) 2017 David Benepe

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
