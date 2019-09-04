cnt = 0
Quicksort([2, 4, 6, 8, 10, 11, 3, 7, 5, 9, 1], 0, 10)
#82 (1, 4, 6, 8, 10, 11, 3, 7, 2, 9, 5)
print(cnt)
cnt = 0
Quicksort([2, 4, 6, 8, 10, 12, 13, 7, 3, 9, 5, 11, 1], 0, 12)
print(cnt)

import itertools

max_cnt = 0
max_cnt_list = []

counter = 0

for item in itertools.permutations([1, 2]):
    cnt = 0
    Quicksort(list(item), 0, len(item) - 1)
    if cnt >= max_cnt:
        max_cnt = cnt
        max_cnt_list.append((item[:],cnt))
    counter = counter + 1
    if counter % 1000000 == 0:
        print(counter)

print(max_cnt)
for it in [i for i in max_cnt_list if i[1] == max_cnt]:
    print(it)
print(max_cnt_list)