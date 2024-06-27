# How binary searches arrive at the destination

The purpose of this repo is to show how differnt variants of the binary search algorithm arrive at a result.

Legend:
 * `L`, `R`, `X` - locations of the left and right pointers, `X` means `L` and `R` are equal.
 * `^` - function's return value.

For example, here are two algorithms lookking for 55, which is a repeated number.
```
Target: 55 (Repeated element), Binary search variant: Python's bisect_left
    6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100  n=25
    L                                                                        R
1                                       L                                    R
2                                       L                 R                 
3                                       L        R                          
4                                       L  R                                
5                                          X                                
                                           ^                                
Returned: 14
Target: 55 (Repeated element), Binary search variant: Python's bisect_right
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

There are two types of files available:
1. `binary_search_<algo-name>_multiple_targets.txt` - this shows how a variant behaves for differnt targets:
   1. [-10](src/binary_searches_ascii/outputs/target_-10_multiple_binary_search_variants.txt) - smaller than the first element,
   2. [6](src/binary_searches_ascii/outputs/target_6_multiple_binary_search_variants.txt) - first element
   3. [50](src/binary_searches_ascii/outputs/target_50_multiple_binary_search_variants.txt)  - a not repeated element not at the edge
   4. [55](src/binary_searches_ascii/outputs/target_55_multiple_binary_search_variants.txt)  - a repeated element not at the edge
   5. [100](src/binary_searches_ascii/outputs/target_100_multiple_binary_search_variants.txt) - last element
   6. [110](src/binary_searches_ascii/outputs/target_110_multiple_binary_search_variants.txt) - greater than the last element
   These files ara great on their own or for comparing them in your favourite text editor. 
2. `target_<target>_multiple_binary_search_variants.txt` - this shows behaviour of different binary search variants for a single target:
   1. [Base](src/binary_searches_ascii/outputs/binary_search_base_multiple_targets.txt) - Base variant from [Wikipedia](https://en.wikipedia.org/wiki/Binary_search),
   2. [Bottenbruch](src/binary_searches_ascii/outputs/binary_search_Bottenbruch_multiple_targets.txt) - Hermann Bottenbruch's version, one fewer 'if' in the loop, from [Wikipedia](https://en.wikipedia.org/wiki/Binary_search),
   3. [bisect_left](src/binary_searches_ascii/outputs/binary_search_bisect_left_python_multiple_targets.txt) - bisect_left from Python 3 https://github.com/python/cpython/blob/main/Lib/bisect.py,  
   4. [bisect_right](src/binary_searches_ascii/outputs/binary_search_bisect_left_python_multiple_targets.txt) - bisect_right from Python 3 https://github.com/python/cpython/blob/main/Lib/bisect.py,




# TODOs

1. Fix print for L<-1 and R>0 (which is not something any of the calls returns);