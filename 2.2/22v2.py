def merge(a, b):
    i, j = 0, 0
    n, m = len(a), len(b)
    c = []
    d = {}
    d1 = {}
    while i < n or j < m:
        if j == m or (i < n and a[i] <= b[j]):
            if (j > 0):
                if i not in d:
                    d[i] = 0
                if (j > 0):
                    d[i] = d[i] + j
            c.append(a[i])
            i = i + 1
        else:
            # if i != len(a):
            #     if i not in d:
            #         d[i] = 0
            #     d[i] = d[i] + 1
            #     # if (j > 0):
            #     #     d[i] = d[i] + 1
            c.append(b[j])
            j = j + 1
    s = sum([i for i in d.values()])
    return c, s

def merge_sort(a, ind):
    n = len(a)
    if n == 1:
        return a, 0
    l = a[0:n//2]
    r = a[n//2:n]
    l, s1 = merge_sort(l,ind)
    r, s2 = merge_sort(r,ind+n//2)
    m, s = merge(l, r)
    return m, s+s1+s2

import time
start = time.time()

with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()
    n = int(lines[0])
    original_list = [int(i) for i in lines[1].split(' ')]

    with open('output.txt', 'w') as OUTPUT:
        A, S = merge_sort(original_list, 0)
        print(S)#49995000
        OUTPUT.write(str(S))

# cnt = 0
# for num in range(0, len(original_list)):
#     cnt1 = 0
#     for num1 in range(num, len(original_list)):
#         if original_list[num] > original_list[num1]:
#             cnt1 = cnt1 + 1
#     cnt = cnt + cnt1
# print(cnt)

end = time.time()

elapsed = end - start
print(elapsed)