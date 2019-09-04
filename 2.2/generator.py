import random

random.seed(5)

with open('input.txt','w') as OUTPUT:
    OUTPUT.write('10000\n')
    l = []
    for i in range(10000, 0, -1):
          l.append(str(i))
    OUTPUT.write(' '.join(l))