using System.Text;

var rand = new Random(Seed: 333);
var n = 50;
var max = 100;

var a = new int[n];
for (int i = 0; i < n; i++)
{
  a[i] = rand.Next(minValue: 0, maxValue: max + 1); // maxValue is exclusive
}
Array.Sort(a);

int target = 55;

int x = BinarySearchBase(a, target);
if (x == -1)
  Console.WriteLine("Not found");
x = BinarySearchLessIfs(a, target);
if (x == -1)
  Console.WriteLine("Not found");

x = binary_search_leftmost(a, target);
if (x == -1)
  Console.WriteLine("Not found");

x = binary_search_rightmost(a, target);
if (x == -1)
  Console.WriteLine("Not found");

Console.WriteLine("TODO Left bisect");  
Console.WriteLine("TODO right bisect");  

Console.WriteLine("TODO Repeat for target 0");  
Console.WriteLine("TODO Print the r at index length for variants that start after the array");


int BinarySearchBase(int[] a, int target)
{
  Console.WriteLine($"Base implementation, target {target}");
  Console.WriteLine($"{string.Join(" ", a)}");

  var l = 0;
  var r = a.Length - 1;
  Print(prefix: null, a, l: l, r: r, printArray: false);

  while (l <= r)
  {
    var mid = (l + r) / 2;
    if (a[mid] == target)
    {
      Print(prefix: null, a, l: mid, r: mid, printArray: false, lrc: '^');
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
    Print(prefix: null, a, l: l, r: r, printArray: false);
  }

  return -1;
}

int BinarySearchLessIfs(int[] a, int target)
{
  Console.WriteLine($"Hermann Bottenbruch version, one less if in the loop, target {target}");
  Console.WriteLine($"{string.Join(" ", a)}");

  var l = 0;
  var r = a.Length - 1;
  Print(prefix: null, a, l: l, r: r, printArray: false);

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
    Print(prefix: null, a, l: l, r: r, printArray: false);
  }
  if (a[l] == target)
  {
     Print(prefix: null, a, l: l, r: l, printArray: false, lrc: '^');
    return l;
  }
  return -1;
}

int binary_search_leftmost(int[] a, int target)
{
  Console.WriteLine($"binary_search_leftmost, target {target}");
  Console.WriteLine($"{string.Join(" ", a)}");

  var l = 0;
  var r = a.Length;
  Print(prefix: null, a, l: l, r: r, printArray: false);

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
    Print(prefix: null, a, l: l, r: r, printArray: false);
  }

  Print(prefix: null, a, l: l, r: l, printArray: false, lrc: '^');
  return l;
}


int binary_search_rightmost(int[] a, int target)
{
  Console.WriteLine($"binary_search_rightmost, target {target}");
  Console.WriteLine($"{string.Join(" ", a)}");

  var l = 0;
  var r = a.Length;
  Print(prefix: null, a, l: l, r: r, printArray: false);

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
    Print(prefix: null, a, l: l, r: r, printArray: false);

  }
  Print(prefix: null, a, l: r-1, r: r-1, printArray: false, lrc: '^');
    
  return r-1;
}



// Print(prefix: "", a, l: 0, r: a.Length - 1, printArray: true);

// for (int i = 0; i < 10; i += 10)
// {
//   Print(prefix: null, a, l: i, r: a.Length - 1, printArray: false, ic: '*');
// }

static void Print(
  string? prefix,
  int[] a,
  int l,
  int r,
  char lc = 'L',
  char rc = 'R',
  char lrc = 'X',
  char ic = ' ',
  bool printArray = true)
{
  StringBuilder sb = new();
  if (printArray)
  {
    sb.AppendLine($"{prefix}{string.Join(" ", a)}, {l}-{r}");
  }

  var prefixlen = prefix?.Length ?? 0; // +1 for space

  for (int i = 0; i < prefixlen; i++)
  {
    sb.Append(' ');
  }
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

  Console.WriteLine(sb.ToString());
}
