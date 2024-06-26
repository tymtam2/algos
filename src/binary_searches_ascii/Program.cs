using System.Runtime.CompilerServices;
using System.Text;

var rand = new Random(Seed: 333);
var n = 25; // Github's UI doesn't use 100% width so a small number will have to do.
var max = 100;

var a = new int[n];
for (int i = 0; i < n; i++)
{
  a[i] = rand.Next(minValue: 0, maxValue: max + 1); // maxValue is exclusive
}
Array.Sort(a);

// return

var binarySearches = new (string Description, string NameForFileName, Func<StringBuilder, int[], int, int> F)[]
{
  ("Wikipedia, base algo", "wikipedia_base", BinarySearchBase),
  ("Wikipedia, Hermann Bottenbruch version, one less if in the loop", "wikipedia_fewerifs", BinarySearchLessIfs),
  ("Wikipedia, binary_search_leftmost", "wikipedia_leftmost", binary_search_leftmost),
  ("Wikipedia, binary_search_rightmost", "wikipedia_rightmost", binary_search_rightmost)
};
Console.WriteLine("TODO Left bisect");
Console.WriteLine("TODO right bisect");

(string Description, int Target)[] targets = [
  ("Smaller than the smallest element", -10),
  ("First element",a[0]),
  ("Repeated element",55),
  ("Last element",a[a.Length-1]),
  ("Greater than the larget element", 110),
  ];

Directory.CreateDirectory("outputs");

// Stage 1. For a number show different algorithms
foreach (var target in targets)
{
  StringBuilder sb = new();
  sb.AppendLine($"Running binary search variants for the same target: {target.Target} ({target.Description})");
  sb.AppendLine();
  foreach (var search in binarySearches)
  {
    Search(sb, a, target, search.F, search.Description);
  }
  File.WriteAllText($"outputs/target_{target.Target}_multiple_binary_search_variants.txt", sb.ToString());
}

// Stage 2. For each algorithm show behaviour for different numbers 
foreach (var search in binarySearches)
{
  StringBuilder sb = new();
  sb.AppendLine($"Running XXX binary search variant: '{search.Description}' for multiple targets");
  sb.AppendLine();
  foreach (var target in targets)
  {
    Search(sb, a, target, search.F, search.Description);
    sb.AppendLine();
  }
  File.WriteAllText($"outputs/binary_search_{search.NameForFileName}_multiple_targets.txt", sb.ToString());
}

static void Search(
  StringBuilder sb,
  int[] a,
  (string Description, int Target) target,
  Func<StringBuilder, int[], int, int> fsearch,
  string searchDescription)
{
  sb.AppendLine($"Target: {target.Target} ({target.Description}), Binary search variant: {searchDescription}");


  int index = fsearch(sb, a, target.Target);
  if (index == -1)
    sb.AppendLine("<Not found>");
  else
    sb.AppendLine("<Found>");
}


int BinarySearchBase(StringBuilder sb, int[] a, int target)
{
  var l = 0;
  var r = a.Length - 1;
  Print(sb, prefix: null, a, l: l, r: r, printArray: true);

  while (l <= r)
  {
    var mid = (l + r) / 2;
    if (a[mid] == target)
    {
      Print(sb, prefix: null, a, l: mid, r: mid, printArray: false, lrc: '^');
      return mid;
    }
    else if (a[mid] < target)
    {
      l = mid + 1;
    }
    else
    {
      r = mid - 1;
    }
    Print(sb, prefix: null, a, l: l, r: r, printArray: false);
  }

  Print(sb, prefix: null, a, l: -1, r: -1, printArray: false, lrc: '^');
  return -1;
}

int BinarySearchLessIfs(StringBuilder sb, int[] a, int target)
{
  var l = 0;
  var r = a.Length - 1;
  Print(sb, prefix: null, a, l: l, r: r, printArray: true);

  while (l != r)
  {
    int mid = (int)Math.Ceiling((decimal)(l + r) / 2);
    if (a[mid] > target)
    {
      r = mid - 1;
    }
    else
    {
      l = mid;
    }
    Print(sb, prefix: null, a, l: l, r: r, printArray: false);
  }
  if (a[l] == target)
  {
    Print(sb, prefix: null, a, l: l, r: l, printArray: false, lrc: '^');
    return l;
  }
  Print(sb, prefix: null, a, l: -1, r: -1, printArray: false, lrc: '^');
  return -1;
}

int binary_search_leftmost(StringBuilder sb, int[] a, int target)
{
  var l = 0;
  var r = a.Length;
  Print(sb, prefix: null, a, l: l, r: r, printArray: true);

  while (l < r)
  {
    var mid = (l + r) / 2;
    if (a[mid] < target)
    {
      l = mid + 1;
    }
    else
    {
      r = mid;
    }
    Print(sb, prefix: null, a, l: l, r: r, printArray: false);
  }

  Print(sb, prefix: null, a, l: l, r: l, printArray: false, lrc: '^');
  return l;
}


int binary_search_rightmost(StringBuilder sb, int[] a, int target)
{
  var l = 0;
  var r = a.Length;
  Print(sb, prefix: null, a, l: l, r: r, printArray: true);

  while (l < r)
  {
    var mid = (l + r) / 2;
    if (a[mid] > target)
    {
      r = mid;
    }
    else
    {
      l = mid + 1;
    }
    Print(sb, prefix: null, a, l: l, r: r, printArray: false);

  }

  Print(sb, prefix: null, a, l: r - 1, r: r - 1, printArray: false, lrc: '^');
  return r - 1;
}

// Test with 
// foreach (var l in new int[] { -10, -1, 0, a.Length - 1, a.Length, a.Length + 10 })
//   foreach (var r in new int[] { -10, -1, 0, a.Length - 1, a.Length, a.Length + 10 })
//   {
//     StringBuilder sb = new ();
//       sb.AppendLine($"----------L={l} R={r}");
//     Print(sb, prefix: null, a, l: l, r: r, printArray: true);
//   }
static void Print(
  StringBuilder sb,
  string? prefix,
  int[] a,
  int l,
  int r,
  char lc = 'L',
  char rc = 'R',
  char lrc = 'X',
  char ic = ' ',
  bool printArray = true,
  bool supportsOutOfBounds = true)
{


  string? outOfBoundPrefix = null;
  int? outOfBoundPrefixPadding = null;
  if (l < 0 && r < 0)
  {
    if (l < -1 && r < -1)
    {
      outOfBoundPrefix = $"<< L:{l},R:{r}";
      outOfBoundPrefixPadding = 0;
    }
    else
    {
      if (l == r)
        outOfBoundPrefix = lrc + " ";
      else
        outOfBoundPrefix = lc + " ";

      outOfBoundPrefixPadding = 2;
    }
  }
  else if (l < 0)
  {
    if (l < -1)
    {
      outOfBoundPrefix = $"L:{l} ";
    }
    else // l == -1
    {
      outOfBoundPrefix = lc + " ";
    }
    outOfBoundPrefixPadding = outOfBoundPrefix.Length;
  }
  else if (r < 0)
  {
    if (r < -1)
    {
      outOfBoundPrefix = $"R:{r} ";
    }
    else // r == -1
    {
      outOfBoundPrefix = rc + " ";
    }
    outOfBoundPrefixPadding = outOfBoundPrefix.Length;
  }
  if (outOfBoundPrefixPadding is null && supportsOutOfBounds)
  {
    outOfBoundPrefixPadding = 2;
  }

  if (printArray)
  {
    sb.Append(prefix);
    if (outOfBoundPrefixPadding != null)
    {
      for (int i = 0; i < outOfBoundPrefixPadding.Value; i++) sb.Append(' ');
    }
    sb.Append(string.Join(" ", a));
    sb.AppendLine();
  }


  var prefixlen = prefix?.Length ?? 0; // +1 for space

  for (int i = 0; i < prefixlen; i++) sb.Append(' ');
  if (outOfBoundPrefix is not null)
    sb.Append(outOfBoundPrefix);
  if (outOfBoundPrefix is null && outOfBoundPrefixPadding is not null)
    for (int i = 0; i < outOfBoundPrefixPadding.Value; i++) sb.Append(' ');

  for (int i = 0; i < a.Length; i++)
  {
    if (i != 0) sb.Append(' ');
    var s = a[i].ToString();
    var sn = s.Length;
    if (i != l && i != r)
    {
      for (int j = 0; j < sn; j++) sb.Append(ic);
    }
    else if (i == l && i == r)
    {
      sb.Append(lrc);
      for (int j = 0; j < sn - 1; j++) sb.Append(' ');
    }
    else if (i == l)
    {
      sb.Append(lc);
      for (int j = 0; j < sn - 1; j++) sb.Append(' ');
    }
    else
    {
      sb.Append(rc);
      for (int j = 0; j < sn - 1; j++) sb.Append(' ');
    }
  }

  string? outOfBoundPostfix = null;
  if (l > (a.Length - 1) && r > (a.Length - 1))
  {
    if (l > a.Length || r > a.Length)
    {
      outOfBoundPostfix = $">> L:{l},R:{r}";
    }
    else
    {
      if (l == r)
        outOfBoundPostfix = " " + lrc;
      else
        outOfBoundPostfix = " " + rc;
    }
  }
  else if (l > a.Length - 1)
  {// Only L
    if (l > a.Length - 1 + 1)
    {
      outOfBoundPostfix = $" L:{l}";
    }
    else // l == a.Length 
    {
      outOfBoundPostfix = " " + lc;
    }
  }
  else if (r > a.Length - 1)
  {

    if (r > a.Length)
    {
      outOfBoundPostfix = $" R:{r}";
    }
    else // l == a.Length 
    {
      outOfBoundPostfix = " " + rc;
    }
  }
  if (outOfBoundPostfix is not null) sb.Append(outOfBoundPostfix);
  sb.AppendLine();
}
