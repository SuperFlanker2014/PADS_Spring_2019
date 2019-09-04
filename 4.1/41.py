lines = []

with open('input.txt','r') as INPUT:
    lines = INPUT.readlines()

M = int(lines[0])
stack = [None]*M
stack_head = -1

with open('output.txt', 'w') as OUTPUT:
    for op in lines[1:]:
        if op.startswith('+'):
            stack_head += 1
            stack[stack_head] = op[2:]
        elif op.startswith('-'):
            OUTPUT.write(stack[stack_head])
            stack_head -= 1
        print(op, stack)