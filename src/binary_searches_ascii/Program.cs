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
// 6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100


var binarySearches = new (string Description, string NameForFileName, Func<StringBuilder, int[], int, int> F)[]
{
  ("Base variant", "base", BinarySearchBase),
  ("Hermann Bottenbruch version, one less if in the loop", "Bottenbruch", BinarySearchBottenbruch),
  ("Python's bisect_left", "bisect_left_python", bisect_left_python),
  ("Python's bisect_right", "bisect_right_python", bisect_right_python)
};

(string Description, int Target)[] targets = [
  ("Smaller than the smallest element", -10),
  ("First element",a[0]),
  ("Not repeated element",50),
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
  sb.AppendLine($"Running variant: '{search.Description}' for multiple targets");
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
  sb.AppendLine($"Returned: {index}");
}


int BinarySearchBase(StringBuilder sb, int[] a, int target)
{
  var l = 0;
  var r = a.Length - 1;
  Print(sb, prefix: "  ", a, l: l, r: r, printArray: true);
  int step = 1;

  while (l <= r)
  {
    var mid = (l + r) / 2;
    if (a[mid] == target)
    {
      Print(sb, prefix: "  ", a, l: mid, r: mid, printArray: false, lrc: '^');
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
    Print(sb, prefix: $"{step++} ", a, l: l, r: r, printArray: false);
  }

  Print(sb, prefix: "  ", a, l: -1, r: -1, printArray: false, lrc: '^');
  return -1;
}

int BinarySearchBottenbruch(StringBuilder sb, int[] a, int target)
{

  var l = 0;
  var r = a.Length - 1; 
  Print(sb, prefix: "  ", a, l: l, r: r, printArray: true);
  int step = 1;

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
    Print(sb, prefix: $"{step++} ", a, l: l, r: r, printArray: false);
  }
  if (a[l] == target)
  {
    Print(sb, prefix: "  ", a, l: l, r: l, printArray: false, lrc: '^');
    return l;
  }
  Print(sb, prefix: "  ", a, l: -1, r: -1, printArray: false, lrc: '^');
  return -1;
}

int bisect_left_python(StringBuilder sb, int[] a, int target)
{
  var lo = 0;
  var hi = a.Length;
  Print(sb, prefix: "  ", a, l: lo, r: hi, printArray: true);
  int step = 1;

  while (lo < hi)
  {
    var mid = (lo + hi) / 2;
    if (a[mid] < target)
    {
      lo = mid + 1;
    }
    else
    {
      hi = mid;
    }
    Print(sb, prefix: $"{step++} ", a, l: lo, r: hi, printArray: false);
  }

  Print(sb, prefix: "  ", a, l: lo, r: lo, printArray: false, lrc: '^');
  return lo;
}


int bisect_right_python(StringBuilder sb, int[] a, int target)
{
  var lo = 0;
  var hi = a.Length;
  Print(sb, prefix: "  ", a, l: lo, r: hi, printArray: true);
  var step = 1;

  while (lo < hi)
  {
    var mid = (lo + hi) / 2;
    if (target < a[mid])
    {
      hi = mid;
    }
    else
    {
      lo = mid + 1;
    }
    Print(sb, prefix: $"{step++} ", a, l: lo, r: hi, printArray: false);

  }

  Print(sb, prefix: "  ", a, l: lo, r: lo, printArray: false, lrc: '^');
  return lo;
}

// Test with 
// foreach (var l in new int[] { -10, -1, 0, a.Length - 1, a.Length, a.Length + 10 })
//   foreach (var r in new int[] { -10, -1, 0, a.Length - 1, a.Length, a.Length + 10 })
//   {
//     StringBuilder sb = new ();
//       sb.AppendLine($"----------L={l} R={r}");
//     Print(sb, prefix: "  ", a, l: l, r: r, printArray: true);
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
  bool supportsOutOfBounds = true,
  bool prefixIsForArrayLine = false)
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
    if(prefixIsForArrayLine)
      sb.Append(prefix);
    else
      for (int i = 0; i < (prefix?.Length ?? 0); i++) sb.Append(' ');

    if (outOfBoundPrefixPadding != null)
    {
      for (int i = 0; i < outOfBoundPrefixPadding.Value; i++) sb.Append(' ');
    }
    sb.Append(string.Join(" ", a));
    sb.Append($"  n={a.Length}");
    sb.AppendLine();
  }

  if (prefix is not null && !prefixIsForArrayLine)
  {
    sb.Append(prefix);
  }
  else
  {
    var prefixlen = prefix?.Length ?? 0;

    for (int i = 0; i < prefixlen; i++) sb.Append(' ');
  }
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
