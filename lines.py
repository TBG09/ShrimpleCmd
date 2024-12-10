import os

def count_lines_and_characters(directory):
    total_lines = 0
    total_characters = 0

    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith(".cs"):
                file_path = os.path.join(root, file)
                with open(file_path, 'r', encoding='utf-8') as f:
                    for line in f:
                        stripped_line = line.strip()
                        # Ignore empty lines and lines with only comments
                        if stripped_line and not stripped_line.startswith("//"):
                            # Remove in-line comments and count characters
                            code_part = stripped_line.split("//")[0]
                            total_characters += len(code_part)
                            total_lines += 1
    return total_lines, total_characters

def main():
    current_directory = os.getcwd()
    lines, characters = count_lines_and_characters(current_directory)
    
    print(f"Total lines (excluding comments and empty lines): {lines}")
    print(f"Total characters (excluding comments): {characters}")
    input("Press Enter to exit...")

if __name__ == "__main__":
    main()
