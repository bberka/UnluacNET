# UnluacNET

A .NET Lua 5.1 decompiler based on the original Java [unluac](http://sourceforge.net/projects/unluac/) by tehtmi.

## Features & Recent Updates

This fork includes several modern improvements and bug fixes over the original repository:

- **Robust Batch Processing:** Seamlessly decompile entire directories of `.luac` files. The application safely handles nested folders and will no longer crash or stop the batch if a single file fails to decompile.
- **Modern CLI:** Rebuilt using `CommandLineParser` for standardized argument parsing, complete with automated `--help` screens and a `--verbose` flag for debugging stack traces.
- **Improved Logging:** Features color-coded console output to easily distinguish between info, warnings, successes, and errors.
- **Encoding Fixes (更改说明):** Fixed Chinese encoding issues for `asname` and `cx` variables. _(Modified from the original [Fireboyd78/UnluacNET](https://github.com/Fireboyd78/UnluacNET) project)._

## Usage

The decompiler can be run from the command line. It accepts both single files and directories.

### Command Line Arguments

```text
  -v, --verbose    Enable verbose logging to see full stack traces on error.
  --help           Display this help screen.
  --version        Display version information.
  Input (pos. 0)   Required. Input .luac file or directory containing .luac files.
  Output (pos. 1)  Required. Output .lua file or directory to save decompiled files.
```
