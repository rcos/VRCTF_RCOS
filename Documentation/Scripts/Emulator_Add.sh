#!/bin/bash

# Define file name
file="../Gyroscope Testing/Library/PackageCache/com.google.xr.cardboard/Runtime/CardboardReticlePointer.cs"
newline="        if (Google.XR.Cardboard.Api.IsTriggerPressed || UnityEngine.Input.GetMouseButtonDown(0)) "

temp_file=$(mktemp)

# Modify line 184 using awk
awk -v new_line="$newline" 'NR==184 {print new_line; next} {print}' "$file" > "$temp_file" && mv "$temp_file" "$file"

echo "Line 184 updated successfully."
read -p "Press any key to continue..." -n1 -s