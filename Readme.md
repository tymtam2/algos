# Binary searches variants

The purpose of this repo is to show how differnt variants of the binary search algorithm arrive at a result.

As an example, here are multiple algorithms looking for 55 which is repeated 3 times in the array.

Legend:
 * `L`, `R`, `X` - locations of the left and right pointers, `X` means `L` and `R` are equal,
 * `^` - function's return value,
 * The number on left is updated **after** each step (1 means one step done).
```
Variant: Base variant, target: 55 (Repeated element)
    6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100  n=25
    L                                                                    R  
1                                       L                                R  
2                                       L           R                       
                                              ^                             
Returned: 15

Variant: Hermann Bottenbruch version, one less if in the loop, target: 55 (Repeated element)
    6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100  n=25
    L                                                                    R  
1                                    L                                   R  
2                                    L              R                       
3                                             L     R                       
4                                                L  R                       
5                                                X                          
                                                 ^                          
Returned: 16

Variant: Python's bisect_left, target: 55 (Repeated element)
    6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100  n=25
    L                                                                        R
1                                       L                                    R
2                                       L                 R                 
3                                       L        R                          
4                                       L  R                                
5                                          X                                
                                           ^                                
Returned: 14

Variant: Python's bisect_right, target: 55 (Repeated element)
    6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100  n=25
    L                                                                        R
1                                       L                                    R
2                                       L                 R                 
3                                                   L     R                 
4                                                   L  R                    
5                                                   X                       
                                                    ^                       
Returned: 17
```
## Two perspectives

There are two types of files available:
1. `binary_search_<algo-name>_multiple_targets.txt` - this shows how a variant behaves for differnt targets:
   1. [-10...txt](src/binary_searches_ascii/outputs/target_-10_multiple_binary_search_variants.txt) - smaller than the first element,
   2. [6...txt](src/binary_searches_ascii/outputs/target_6_multiple_binary_search_variants.txt) - first element
   3. [50...txt](src/binary_searches_ascii/outputs/target_50_multiple_binary_search_variants.txt)  - a not repeated element not at the edge
   4. [55...txt](src/binary_searches_ascii/outputs/target_55_multiple_binary_search_variants.txt)  - a repeated element not at the edge
   5. [100...txt](src/binary_searches_ascii/outputs/target_100_multiple_binary_search_variants.txt) - last element
   6. [110...txt](src/binary_searches_ascii/outputs/target_110_multiple_binary_search_variants.txt) - greater than the last element
   These files ara great on their own or for comparing them in your favourite text editor. 
2. `target_<target>_multiple_binary_search_variants.txt` - this shows behaviour of different binary search variants for a single target:
   1. [Base..txt](src/binary_searches_ascii/outputs/binary_search_base_multiple_targets.txt) - Base variant from [Wikipedia](https://en.wikipedia.org/wiki/Binary_search),
   2. [Bottenbruch...txt](src/binary_searches_ascii/outputs/binary_search_Bottenbruch_multiple_targets.txt) - Hermann Bottenbruch's version, one fewer 'if' in the loop, from [Wikipedia](https://en.wikipedia.org/wiki/Binary_search),
   3. [bisect_left...txt](src/binary_searches_ascii/outputs/binary_search_bisect_left_python_multiple_targets.txt) - [bisect_left](https://github.com/python/cpython/blob/main/Lib/bisect.py) from Python 3, 
      > Return the index where to insert item x in list a, assuming a is sorted.
      >
      > The return value i is such that all e in a[:i] have e < x, and all e in
        a[i:] have e >= x.  So if x already appears in the list, a.insert(i, x) will
        insert just before the leftmost x already there.
   4. [bisect_right...txt](src/binary_searches_ascii/outputs/binary_search_bisect_left_python_multiple_targets.txt) - [bisect_right](https://github.com/python/cpython/blob/main/Lib/bisect.py) from Python 3:
      > Return the index where to insert item x in list a, assuming a is sorted.
      >
      > The return value i is such that all e in a[:i] have e <= x, and all e in
        a[i:] have e > x.  So if x already appears in the list, a.insert(i, x) will
        insert just after the rightmost x already there.




# TODOs

1. Fix print for L<-1 and R>0 (which is not something any of the calls returns);
2. Make print 2 stage:
   1. n times call "collect" -> collect l and r 
   2. call print(steps, ...) at the end 
   This way we can have a nicer output and maybe draw vertical lines to Ls and Rs  