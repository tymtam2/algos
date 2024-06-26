More to come


# How binary searches arrive at the destination

## Searching for an element which exists in the collection

```
Base implementation, target 55
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                            R  
                                                                     L                                                                       R  
                                                                     L                                R                                         
                                                                                       L              R                                         
                                                                                             ^                                                  
Hermann Bottenbruch version, one less if in the loop, target 55
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                            R  
                                                                     L                                                                       R  
                                                                     L                                R                                         
                                                                                       L              R                                         
                                                                                       L     R                                                  
                                                                                          L  R                                                  
                                                                                             X                                                  
                                                                                             ^                                                  
binary_search_leftmost, target 55
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                               
                                                                        L                                                                       
                                                                        L                                   R                                   
                                                                        L                 R                                                     
                                                                                    L     R                                                     
                                                                                    L  R                                                        
                                                                                       X                                                        
                                                                                       ^                                                        
binary_search_rightmost, target 55
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                               
                                                                        L                                                                       
                                                                        L                                   R                                   
                                                                                             L              R                                   
                                                                                             L     R                                            
                                                                                             L  R                                               
                                                                                                X                                               
                                                                                             ^           
```

## Searching for non-existing element 
```
Base implementation, target 0
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                            R  
L                                                              R                                                                                
L                       R                                                                                                                       
L       R                                                                                                                                       
L R                                                                                                                                             
L                                                                                                                                               
Not found
Hermann Bottenbruch version, one less if in the loop, target 0
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                            R  
L                                                                 R                                                                             
L                          R                                                                                                                    
L         R                                                                                                                                     
L   R                                                                                                                                           
X                                                                                                                                               
Not found
binary_search_leftmost, target 0
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                               
L                                                                    R                                                                          
L                             R                                                                                                                 
L           R                                                                                                                                   
L     R                                                                                                                                         
L R                                                                                                                                             
X                                                                                                                                               
^                                                                                                                                               
binary_search_rightmost, target 0
1 3 4 6 6 7 10 12 14 16 17 19 20 23 23 23 25 26 27 32 33 34 36 37 44 45 48 50 53 53 53 55 55 55 57 62 63 64 65 68 68 69 70 71 72 85 87 92 96 100
L                                                                                                                                               
L                                                                    R                                                                          
L                             R                                                                                                                 
L           R                                                                                                                                   
L     R                                                                                                                                         
L R                                                                                                                                             
X                                                                                                                                               
                                                                                                                                                
Not found
```


# TODOs

1. Create:
  1. All algos for:
      1.  -2 smaller than the first, not included
      2.  0
      3.  55 
      4.  100 (last item)
      5.  102 bigger than the last, not included
  2. How each algorithm behaves for the values above (same as 1. but from a different perspective) 
2. Maybe fix print for L<-1 and R>0;
3. Investigate why binary_search_leftmost never returns -1 (rightmost does)