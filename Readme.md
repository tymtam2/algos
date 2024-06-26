# How binary searches arrive at the destination

See src/binary_searches_ascii/outputs/ to see how differnt binary search variants arrive at a result.

For example here are two algorithms lookking for 55, which is a repeated number.

```
Target: 55 (Repeated element), Binary search variant: Wikipedia, binary_search_leftmost
  1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
  L                                                                                                                                                R
                                                                          L                                                                        R
                                                                          L                                   R                                   
                                                                          L                 R                                                     
                                                                                      L     R                                                     
                                                                                      L  R                                                        
                                                                                         X                                                        
                                                                                         ^                                                        
<Found>
Target: 55 (Repeated element), Binary search variant: Wikipedia, binary_search_rightmost
  1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
  L                                                                                                                                                R
                                                                          L                                                                        R
                                                                          L                                   R                                   
                                                                                               L              R                                   
                                                                                               L     R                                            
                                                                                               L  R                                               
                                                                                                  X                                               
                                                                                               ^                                                  
<Found>
```


# TODOs

1. Investigate why binary_search_leftmost never returns -1 (rightmost does)
2. Maybe fix print for L<-1 and R>0;