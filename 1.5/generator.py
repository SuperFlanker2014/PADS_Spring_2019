import random

random.seed(5)

with open('input.txt','w') as OUTPUT:
    OUTPUT.write('5000\n')
    l = []
    for i in range(5000):
        n = random.randrange(1, 20001)
        l.append(str(n))
    OUTPUT.write(' '.join(l))