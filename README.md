# CSharpRayTracing
If you want to mess around with rendering, I've implemented spheres and triangles to be rendered
All objects to render must be in the list of ISurfaces at the top of scene.cs
Spheres take these parameters: Sphere(x, y, z, radius, color, shininess)
Triangles (called Planes) take these: Plane(x, y, z, Vector(x, y, z), Vector(x, y, z), color, shininess)

If you'd like to add a new surface, like an elipsoid or maybe create a cube from 12 triangles, make sure the class is part of the interface ISurface. It must have a method called Touch() and one called ColorMod() You can copy ColorMod() from one of the other surfaces, but Touch() will be unique and must return the distance to the object, and the vector reflected off of the object.
