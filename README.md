<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/JMVRy/AimTrainer-HB">
    <img src="images/Logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">AimTrainer Cheat</h3>

  <p align="center">
    A tool to cheat at the <a href="https://humanbenchmark.com/tests/aim">Aim Trainer on HumanBenchmark.com</a>
    <br />
    <a href="https://github.com/JMVRy/AimTrainer-HB/wiki/Documentation"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="#usage">View Demo</a>
    ·
    <a href="https://github.com/JMVRy/AimTrainer-HB/issues">Report Bug</a>
    ·
    <a href="https://github.com/JMVRy/AimTrainer-HB/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]][repo-url]

This project is made to cheat at the [Aim Trainer on HumanBenchmark.com][trainer-hb] using Windows' Win32 API for moving the mouse and clicking the targets. It's not extremely fast or anything, moreso just a way to expand my experience with C# and figure out how to use external libraries for my own projects.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* [![C#][CSharp]][CSharp-url]
* [ScreenCapture.NET][ScreenCapture.NET]

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To run the program on your own machine, follow the following steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
This is a list on all the things you need, in order to run the software on your own machine
* Windows 7/8/10/11/Above
  * Any Windows version should work, but I'd suggest 7 or above, because I can't be certain if any version below 7 will work.
* Visual Studio 2022
  * Go to https://visualstudio.microsoft.com and download Visual Studio 2022
* NuGet
  * Visual Studio 2022 comes pre-installed with NuGet, but if you're using something else like VSCode, then Google or DuckDuckGo is your best friend.
* ScreenCapture.NET
  * This project utilizes a NuGet package called ScreenCapture.NET by DarthAffe, which can also be found at [this GitHub repository](https://github.com/DarthAffe/ScreenCapture.NET) or at [this NuGet package][ScreenCapture.NET].
* ScreenCapture.NET.DX11
  * This project also currently utilizes ScreenCapture.NET.DX11, which is a DirectX 11 specific way of capturing a screenshot, found at [this NuGet package][ScreenCapture.NET.DX11] or [their GitHub repository](https://github.com/DarthAffe/ScreenCapture.NET).

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/JMVRy/AimTrainer-HB.git
   ```
1. Install NuGet packages
   ```
   In Visual Studio, go to Project>Manage NuGet packages... and install ScreenCapture.NET.DX11
   ```
   or
   ```sh
   NuGet\Install-Package ScreenCapture.NET.DX11
   ```
   or
   ```sh
   dotnet add package ScreenCapture.NET.DX11
   ```
1. Change the Platform target
   ```
   In Visual Studio, go to Project>[Your project name] Properties and change "Platform target" to x64. I am unsure if x86 is supported.
   ```
1. Change any pixel locations that are different for your own machine
   * Because this project was made for my own machine and the exact locations and sizes of my monitors, it may not work as well for your own computer. As long as you are using Firefox, have the Bookmark Toolbar shown, keep the screen Maximized but not Fullscreen, use a 1080p monitor (as well as making sure that this is the last monitor if you use multiple), possibly even have the toolbar hidden, have the Windows Night Light turned off (turns the screen more orange/red), and keep the webpage at 100% zoom, then it should work fine.
   * If your system does not support DirectX 11, then I suggest trying [ScreenCapture.NET.X11](https://www.nuget.org/packages/ScreenCapture.NET.X11) or [ScreenCapture.NET.DX9](https://www.nuget.org/packages/ScreenCapture.NET.DX9), then changing any code mentioning "DX11" to whichever you've chosen.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

This project is primarily made for the [Aim Trainer on HumanBenchmark][trainer-hb], as well as being specific to my own monitor setup. If you've followed the Installation setup and there's no more problems, then it should just be plug-and-play. Build and run, and any pixel with that specific shade of blue (`#95c3e8`) will be clicked, as long as it's within the 1000x400 box that contains the targets.

_For examples, please refer to the [Documentation](https://github.com/JMVRy/AimTrainer-HB/wiki/Documentation)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/JMVRy/AimTrainer-HB/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the GNU General Public License Version 3. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

JohnMarc Everly Jr. - [LinkedIn][linkedin-url] - srjr18@gmail.com

Project Link: [https://github.com/JMVRy/AimTrainer-HB](https://github.com/JMVRy/AimTrainer-HB)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Microsoft for the Operating System](https://microsoft.com)
* [Windows for the API](https://microsoft.com/en-us/windows)
* [DarthAffe for the Screenshotting tool](https://github.com/DarthAffe)
* [ShareX for telling me exact pixel locations to help with exactly where to screenshot](https://getsharex.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/JMVRy/AimTrainer-HB.svg?style=for-the-badge
[contributors-url]: https://github.com/JMVRy/AimTrainer-HB/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/JMVRy/AimTrainer-HB.svg?style=for-the-badge
[forks-url]: https://github.com/JMVRy/AimTrainer-HB/network/members
[stars-shield]: https://img.shields.io/github/stars/JMVRy/AimTrainer-HB.svg?style=for-the-badge
[stars-url]: https://github.com/JMVRy/AimTrainer-HB/stargazers
[issues-shield]: https://img.shields.io/github/issues/JMVRy/AimTrainer-HB.svg?style=for-the-badge
[issues-url]: https://github.com/JMVRy/AimTrainer-HB/issues
[license-shield]: https://img.shields.io/github/license/JMVRy/AimTrainer-HB.svg?style=for-the-badge
[license-url]: https://github.com/JMVRy/AimTrainer-HB/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/johnmarc-everly-jr-882021225

[ScreenCapture.NET]: https://www.nuget.org/packages/ScreenCapture.NET
[ScreenCapture.NET.DX11]: https://www.nuget.org/packages/ScreenCapture.NET.DX11

[product-screenshot]: images/Screenshot.png

<!-- Product images and URLs -->
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
[CSharp]: https://img.shields.io/badge/csharp-512BD4?style=for-the-badge&logo=csharp&color=512BD4
[CSharp-url]: https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/

[repo-url]: https://github.com/JMVRy/AimTrainer-HB

[trainer-hb]: https://humanbenchmark.com/tests/aim
