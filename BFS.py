
from collections import deque, namedtuple


State = namedtuple('State', ['player_pos', 'boxes_pos'])

def is_goal_state(state, goal_positions):
    return set(state.boxes_pos) == set(goal_positions)

def get_neighbors(state, walls):
    neighbors = []
    directions = [(-1, 0), (1, 0), (0, -1), (0, 1)]  # up, down, left, right
    
    def move(pos, direction):
        return (pos[0] + direction[0], pos[1] + direction[1])
    
    def valid_position(pos):
        return pos not in walls

    player_pos, boxes_pos = state.player_pos, set(state.boxes_pos)

    for direction in directions:
        new_player_pos = move(player_pos, direction)
        
        if new_player_pos in walls:
            continue
        
        if new_player_pos in boxes_pos:
            new_box_pos = move(new_player_pos, direction)
            if new_box_pos in walls or new_box_pos in boxes_pos:
                continue
            new_boxes_pos = tuple(new_box_pos if box == new_player_pos else box for box in state.boxes_pos)
            neighbors.append(State(new_player_pos, new_boxes_pos))
        else:
            neighbors.append(State(new_player_pos, state.boxes_pos))
        print("Hi!")
    return neighbors

def bfs(initial_state, goal_positions, walls):
    queue = deque([initial_state])
    visited = set([initial_state])
    came_from = {initial_state: None}

    while queue:
        current_state = queue.popleft()
        
        if is_goal_state(current_state, goal_positions):
            path = []
            while current_state is not None:
                path.append(current_state)
                current_state = came_from[current_state]
            return path[::-1]
        
        for neighbor in get_neighbors(current_state, walls):
            if neighbor not in visited:
                visited.add(neighbor)
                queue.append(neighbor)
                came_from[neighbor] = current_state
    
    return None




initial_state = State(player_pos=(6, 1), boxes_pos=((3, 2), (5, 2), (4, 3), (3, 4), (6, 4)))
goal_positions = ((3, 1), (2, 3), (2, 4), (2, 6), (3, 4))
walls = {(0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (0, 5) , (0, 6), (0, 7), (0, 8),
         (1, 0), (1, 8),
         (2, 0), (2, 8),
         (3, 0), (3, 8),
         (4, 0), (4, 8),
         (5, 0), (5, 8),
         (6, 0), (6, 8),
         (7, 0), (7, 8),
         (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5), (8, 6), (8, 7), (8, 8), (1, 1), 
         (2, 1), (1, 2), (1, 3), (1, 4), (1, 5), (4, 2), (4, 4), (6, 2), (7, 1), (7, 2), 
         (4, 6), (4, 7), (5, 6), (5, 7), (6, 6), (6 ,7), (7, 6), (7, 7), (6, 5), (7, 5)}

path = bfs(initial_state, goal_positions, walls)

c = 0
if path:
    for step in path:
        c += 1
        print(step)
else:
    print("No solution found.")

print(f"AHAHAHA  {c} ")