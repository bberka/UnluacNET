using CommandLine;
using UnluacNET.Core;
using UnluacNET.Core.Decompile;
using UnluacNET.Core.Parse;

namespace UnluacNET.CLI;

public class Options
{
    [Value(0, MetaName = "Input", Required = true, HelpText = "Input .luac file or directory containing .luac files.")]
    public required string Input { get; set; }

    [Value(1, MetaName = "Output", Required = true, HelpText = "Output .lua file or directory to save decompiled files.")]
    public required string Output { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Enable verbose logging to see full stack traces on error.")]
    public bool Verbose { get; set; }
}

internal class Program
{
    private static readonly string Version = typeof(LuaVersion).Assembly.GetName()
        .Version?.ToString() ?? "1.0";

    private static void Main(string[] args)
    {
        // 2. Parse arguments using CommandLineParser
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunDecompiler)
            .WithNotParsed(_ =>
            {
                // CommandLineParser automatically prints usage/help if parsing fails
                Environment.Exit(1);
            });
    }

    private static void RunDecompiler(Options opts)
    {
        LogInfo($"UnluacNET Decompiler v{Version} started.");

        var input = opts.Input;
        var output = opts.Output;

        if (Directory.Exists(input))
        {
            ProcessDirectory(input, output, opts);
        }
        else if (File.Exists(input))
        {
            ProcessSingleFile(input, output, opts);
        }
        else
        {
            LogError($"Input path does not exist: {input}");
            Environment.Exit(1);
        }

        LogInfo("Execution completed.");
    }

    private static void ProcessDirectory(
        string inputDir,
        string outputDir,
        Options opts
    )
    {
        var files = Directory.GetFiles(inputDir, "*.luac", SearchOption.AllDirectories);

        if (files.Length == 0)
        {
            LogWarn($"No .luac files found in directory: {inputDir}");
            return;
        }

        LogInfo($"Found {files.Length} .luac files. Starting batch decompilation...");

        var successCount = 0;
        var failCount = 0;

        foreach (var file in files)
            try
            {
                // Safely calculate the relative path to maintain folder structures
                var relativePath = file.Substring(inputDir.Length)
                    .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                var outputFilePath = Path.Combine(outputDir, Path.ChangeExtension(relativePath, ".lua"));
                var currentOutputDir = Path.GetDirectoryName(outputFilePath);
                ArgumentException.ThrowIfNullOrEmpty(currentOutputDir);
                if (!Directory.Exists(currentOutputDir)) Directory.CreateDirectory(currentOutputDir);

                if (ProcessSingleFile(file, outputFilePath, opts))
                    successCount++;
                else
                    failCount++;
            }
            catch (Exception ex)
            {
                LogError($"Unexpected error processing '{file}': {ex.Message}");
                failCount++;
            }

        Console.WriteLine();
        LogInfo($"Batch Complete! Success: {successCount} | Failed: {failCount}");
    }

    /// <summary>
    ///     Decompiles a single file. Returns true if successful, false if it fails.
    ///     It no longer crashes the entire application on failure.
    /// </summary>
    private static bool ProcessSingleFile(
        string inputPath,
        string outputPath,
        Options opts
    )
    {
        try
        {
            var lMain = FileToFunction(inputPath);
            var decompiler = new Decompiler(lMain);
            decompiler.Decompile();

            using (var writer = new StreamWriter(outputPath))
            {
                decompiler.Print(new Output(writer));
                writer.Flush();
            }

            if (opts.Verbose) LogSuccess($"Decompiled: '{inputPath}' -> '{outputPath}'");

            return true;
        }
        catch (Exception ex)
        {
            LogError($"Failed to decompile '{inputPath}': {ex.Message}");
            if (opts.Verbose) Console.WriteLine(ex.StackTrace);

            return false; // Return false instead of using Environment.Exit()
        }
    }

    private static LFunction FileToFunction(string fn)
    {
        using var fs = File.Open(fn, FileMode.Open, FileAccess.Read, FileShare.Read);
        var header = new BHeader(fs);
        return header.Function.Parse(fs, header);
    }

    #region Logging Utilities

    private static void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[INFO] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    private static void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[SUCCESS] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    private static void LogWarn(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[WARN] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    private static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[ERROR] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    #endregion
}