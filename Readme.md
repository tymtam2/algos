# How binary searches arrive at the destination

See [src/binary_searches_ascii/outputs/](src/binary_searches_ascii/outputs/) to see how differnt binary search variants arrive at a result.

Legend:
 * `L`, `R`, `X` - locations of the left and right pointers, `X` means `L` and `R` are equal.
 * `^` - function's return value.

For example, here are two algorithms lookking for 55, which is a repeated number.
```
Target: 55 (Repeated element), Binary search variant: Wikipedia, binary_search_leftmost
  6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100
  L                                                                        R
                                      L                                    R
                                      L                 R                 
                                      L        R                          
                                      L  R                                
                                         X                                
                                         ^                                
<Found>
Target: 55 (Repeated element), Binary search variant: Wikipedia, binary_search_rightmost
  6 6 7 10 17 25 27 33 36 44 45 50 53 53 55 55 55 57 62 64 69 70 72 85 100
  L                                                                        R
                                      L                                    R
                                      L                 R                 
                                                  L     R                 
                                                  L  R                    
                                                  X                       
                                               ^                          
<Found>
```

There are two types of files available:
1. `binary_search_<algo-name>_multiple_targets.txt` - this shows how a variant behaves for differnt targets:
   1. Smaller than the first element
   2. Equal to first element
   3. Equal to a repeated element
   4. Equal to last element
   5. Greater than the last element
   These files ara great on their own or for comparing them in your favourite text editor. 
2. `target_<target>_multiple_binary_search_variants.txt` - this shows behaviour of different binary search variants for a single target  



# TODOs

1. Investigate why binary_search_leftmost never returns -1 (rightmost does)
2. Maybe fix print for L<-1 and R>0;