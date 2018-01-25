# Sneyed/GGJ2018 Wiki Docs

Automatically compiled documentation based on c# source using Doxygen

Doxygen theme used is "bootstrapped" from [here](https://github.com/Velron/doxygen-bootstrapped)

## Usage

To open the docs locally:

1. Navigate to `<root>\ctrl`
2. Run openDocs.cmd

## Development

To re-generate or add new documentation, you will need to install Doxygen on your machine

### Automatic

To install Doxygen automatically:

1. Navigate to `<root>\ctrl`
2. Run docsInstallEditTools.cmd

### Manual

To install Doxygen manually:

1. [Go here](http://www.stack.nl/~dimitri/doxygen/download.html)
2. Download one of the releases
   Note: Latest version didn't work for me, i had to go back one version
3. Put the executables in your programs folder and add them to your PATH
4. Start a new terminal session
5. Confirm Doxygen is installed properly: `doxygen -v`

To build the project documentation manually:

1. Navigate to `<root>\docs\src`
2. Execute `doxygen Doxyfile`