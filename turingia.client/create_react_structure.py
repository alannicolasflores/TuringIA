import os
def create_file_with_content(path, filename, content=""):
    """Creates a file at the specified path with the given filename and writes optional content."""
    full_path = os.path.join(path, filename)
    with open(full_path, 'w') as file:
        file.write(content)
    print(f"Created file: {full_path}")

def create_directory_structure_with_apis(base_dir):
    """Creates the directory structure for a React project including API controllers."""
    folders_and_files = {
        "components": ["Header.jsx", "Footer.jsx", "Button.jsx"],
        "pages": ["Home.jsx", "About.jsx", "Contact.jsx"],
        "hooks": ["useFetch.js", "useAuth.js"],
        "context": ["AuthContext.jsx"],
        "services": ["api.js"],
        "controllers": [
            "EstadosController.js",
            "CondadosController.js",
            "HospitalizacionesController.js",
            "CasosDiariosController.js",
            "AuthController.js",
            "UsuariosController.js",
        ],
        "utils": ["helpers.js", "constants.js"],
        "styles": ["global.css", "variables.css"],
    }

    controllers_content = {
        "EstadosController.js": "// Functions to interact with the Estados API endpoints\n",
        "CondadosController.js": "// Functions to interact with the Condados API endpoints\n",
        "HospitalizacionesController.js": "// Functions to interact with the Hospitalizaciones API endpoints\n",
        "CasosDiariosController.js": "// Functions to interact with the Casos Diarios API endpoints\n",
        "AuthController.js": "// Functions to handle authentication-related API endpoints\n",
        "UsuariosController.js": "// Functions to interact with the Usuarios API endpoints\n",
    }

    for folder, files in folders_and_files.items():
        folder_path = os.path.join(base_dir, folder)
        os.makedirs(folder_path, exist_ok=True)
        print(f"Created directory: {folder_path}")
        
        for file in files:
            content = controllers_content.get(file, "")
            create_file_with_content(folder_path, file, content)

if __name__ == "__main__":
    base_directory = os.path.join(os.getcwd(), "src")
    if not os.path.exists(base_directory):
        os.makedirs(base_directory)
        print(f"Created base directory: {base_directory}")

    # Generate the required directory structure with API controllers
    create_directory_structure_with_apis(base_directory)

    print("Project structure with API controllers successfully created!")
