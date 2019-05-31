// Compiled shader for Lumin

//////////////////////////////////////////////////////////////////////////
// 
// NOTE: This is *not* a valid shader file, the contents are provided just
// for information and for debugging purposes only.
// 
//////////////////////////////////////////////////////////////////////////
// Skipping shader variants that would not be included into build of current scene.

Shader "ARCore/ARBackground" {
Properties {
 _MainTex ("Texture", 2D) = "white" { }
 _UvTopLeftRight ("UV of top corners", Vector) = (0.000000,1.000000,1.000000,1.000000)
 _UvBottomLeftRight ("UV of bottom corners", Vector) = (0.000000,0.000000,1.000000,0.000000)
}
SubShader { 
 Pass {
  ZWrite Off
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Global Keywords: <none>
Local Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "gles3":
Shader Disassembly:
#version 300 es
#extension GL_OES_EGL_image_external_essl3 : require
#define UNITY_NO_DXT5nm 1
#define UNITY_NO_RGBM 1
#define UNITY_ENABLE_REFLECTION_BUFFERS 1
#define UNITY_FRAMEBUFFER_FETCH_AVAILABLE 1
#define UNITY_NO_CUBEMAP_ARRAY 1
#define UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS 1
#define UNITY_PBS_USE_BRDF1 1
#define UNITY_SPECCUBE_BOX_PROJECTION 1
#define UNITY_SPECCUBE_BLENDING 1
#define UNITY_ENABLE_DETAIL_NORMALMAP 1
#define SHADER_API_MOBILE 1
#define UNITY_LIGHTMAP_DLDR_ENCODING 1
#ifndef SHADER_TARGET
    #define SHADER_TARGET 25
#endif
#ifndef SHADER_REQUIRE_DERIVATIVES
    #define SHADER_REQUIRE_DERIVATIVES 1
#endif
#ifndef SHADER_TARGET_AVAILABLE
    #define SHADER_TARGET_AVAILABLE 35
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS10
    #define SHADER_AVAILABLE_INTERPOLATORS10 1
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS15
    #define SHADER_AVAILABLE_INTERPOLATORS15 1
#endif
#ifndef SHADER_AVAILABLE_INTEGERS
    #define SHADER_AVAILABLE_INTEGERS 1
#endif
#ifndef SHADER_AVAILABLE_MRT4
    #define SHADER_AVAILABLE_MRT4 1
#endif
#ifndef SHADER_AVAILABLE_DERIVATIVES
    #define SHADER_AVAILABLE_DERIVATIVES 1
#endif
#ifndef SHADER_AVAILABLE_SAMPLELOD
    #define SHADER_AVAILABLE_SAMPLELOD 1
#endif
#ifndef SHADER_AVAILABLE_FRAGCOORD
    #define SHADER_AVAILABLE_FRAGCOORD 1
#endif
#ifndef SHADER_AVAILABLE_2DARRAY
    #define SHADER_AVAILABLE_2DARRAY 1
#endif
#ifndef SHADER_AVAILABLE_INSTANCING
    #define SHADER_AVAILABLE_INSTANCING 1
#endif
#ifndef SHADER_API_GLES3
    #define SHADER_API_GLES3 1
#endif
#line 1
#ifndef GLSL_SUPPORT_INCLUDED
#define GLSL_SUPPORT_INCLUDED

// Automatically included in raw GLSL (GLSLPROGRAM) shader snippets, to map from some of the legacy OpenGL
// variable names to uniform names used by Unity.

#ifdef GL_FRAGMENT_PRECISION_HIGH
    precision highp float;
#else
    precision mediump float;
#endif

uniform mat4 unity_ObjectToWorld;
uniform mat4 unity_WorldToObject;
uniform mat4 unity_MatrixVP;
uniform mat4 unity_MatrixV;
uniform mat4 unity_MatrixInvV;
uniform mat4 glstate_matrix_projection;

#define gl_ModelViewProjectionMatrix        (unity_MatrixVP * unity_ObjectToWorld)
#define gl_ModelViewMatrix                  (unity_MatrixV * unity_ObjectToWorld)
#define gl_ModelViewMatrixTranspose         (transpose(unity_MatrixV * unity_ObjectToWorld))
#define gl_ModelViewMatrixInverseTranspose  (transpose(unity_WorldToObject * unity_MatrixInvV))
#define gl_NormalMatrix                     (transpose(mat3(unity_WorldToObject * unity_MatrixInvV)))
#define gl_ProjectionMatrix                 glstate_matrix_projection

#if __VERSION__ < 120
#ifndef UNITY_GLSL_STRIP_TRANSPOSE
mat3 transpose(mat3 mtx)
{
    vec3 c0 = mtx[0];
    vec3 c1 = mtx[1];
    vec3 c2 = mtx[2];

    return mat3(
        vec3(c0.x, c1.x, c2.x),
        vec3(c0.y, c1.y, c2.y),
        vec3(c0.z, c1.z, c2.z)
    );
}
mat4 transpose(mat4 mtx)
{
    vec4 c0 = mtx[0];
    vec4 c1 = mtx[1];
    vec4 c2 = mtx[2];
    vec4 c3 = mtx[3];

    return mat4(
        vec4(c0.x, c1.x, c2.x, c3.x),
        vec4(c0.y, c1.y, c2.y, c3.y),
        vec4(c0.z, c1.z, c2.z, c3.z),
        vec4(c0.w, c1.w, c2.w, c3.w)
    );
}
#endif
#endif // __VERSION__ < 120

#endif // GLSL_SUPPORT_INCLUDED

#line 53

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif
/* UNITY: Original start of shader */

            // #pragma only_renderers gles3

            #ifdef SHADER_API_GLES3
            
            #endif

            uniform vec4 _UvTopLeftRight;
            uniform vec4 _UvBottomLeftRight;

            
            
            

#ifdef VERTEX
#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;


            out vec2 textureCoord;

            void main()
            {
                #ifdef SHADER_API_GLES3
                vec2 uvTop = mix(_UvTopLeftRight.xy, _UvTopLeftRight.zw, gl_MultiTexCoord0.x);
                vec2 uvBottom = mix(_UvBottomLeftRight.xy, _UvBottomLeftRight.zw, gl_MultiTexCoord0.x);
                textureCoord = mix(uvTop, uvBottom, gl_MultiTexCoord0.y);

                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                #endif
            }

            
#endif
#ifdef FRAGMENT
#define gl_FragColor _glesFragColor
layout(location = 0) out mediump vec4 _glesFragColor;

precision highp float;

            in vec2 textureCoord;
            uniform samplerExternalOES _MainTex;

            void main()
            {
                #ifdef SHADER_API_GLES3
                gl_FragColor = texture(_MainTex, textureCoord);
                #endif
            }

            
#endif

-- Hardware tier variant: Tier 1
-- Fragment shader for "gles3":
Shader Disassembly:
#version 300 es
#extension GL_OES_EGL_image_external_essl3 : require
#define UNITY_NO_DXT5nm 1
#define UNITY_NO_RGBM 1
#define UNITY_ENABLE_REFLECTION_BUFFERS 1
#define UNITY_FRAMEBUFFER_FETCH_AVAILABLE 1
#define UNITY_NO_CUBEMAP_ARRAY 1
#define UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS 1
#define UNITY_PBS_USE_BRDF1 1
#define UNITY_SPECCUBE_BOX_PROJECTION 1
#define UNITY_SPECCUBE_BLENDING 1
#define UNITY_ENABLE_DETAIL_NORMALMAP 1
#define SHADER_API_MOBILE 1
#define UNITY_LIGHTMAP_DLDR_ENCODING 1
#ifndef SHADER_TARGET
    #define SHADER_TARGET 25
#endif
#ifndef SHADER_REQUIRE_DERIVATIVES
    #define SHADER_REQUIRE_DERIVATIVES 1
#endif
#ifndef SHADER_TARGET_AVAILABLE
    #define SHADER_TARGET_AVAILABLE 35
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS10
    #define SHADER_AVAILABLE_INTERPOLATORS10 1
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS15
    #define SHADER_AVAILABLE_INTERPOLATORS15 1
#endif
#ifndef SHADER_AVAILABLE_INTEGERS
    #define SHADER_AVAILABLE_INTEGERS 1
#endif
#ifndef SHADER_AVAILABLE_MRT4
    #define SHADER_AVAILABLE_MRT4 1
#endif
#ifndef SHADER_AVAILABLE_DERIVATIVES
    #define SHADER_AVAILABLE_DERIVATIVES 1
#endif
#ifndef SHADER_AVAILABLE_SAMPLELOD
    #define SHADER_AVAILABLE_SAMPLELOD 1
#endif
#ifndef SHADER_AVAILABLE_FRAGCOORD
    #define SHADER_AVAILABLE_FRAGCOORD 1
#endif
#ifndef SHADER_AVAILABLE_2DARRAY
    #define SHADER_AVAILABLE_2DARRAY 1
#endif
#ifndef SHADER_AVAILABLE_INSTANCING
    #define SHADER_AVAILABLE_INSTANCING 1
#endif
#ifndef SHADER_API_GLES3
    #define SHADER_API_GLES3 1
#endif
#line 1
#ifndef GLSL_SUPPORT_INCLUDED
#define GLSL_SUPPORT_INCLUDED

// Automatically included in raw GLSL (GLSLPROGRAM) shader snippets, to map from some of the legacy OpenGL
// variable names to uniform names used by Unity.

#ifdef GL_FRAGMENT_PRECISION_HIGH
    precision highp float;
#else
    precision mediump float;
#endif

uniform mat4 unity_ObjectToWorld;
uniform mat4 unity_WorldToObject;
uniform mat4 unity_MatrixVP;
uniform mat4 unity_MatrixV;
uniform mat4 unity_MatrixInvV;
uniform mat4 glstate_matrix_projection;

#define gl_ModelViewProjectionMatrix        (unity_MatrixVP * unity_ObjectToWorld)
#define gl_ModelViewMatrix                  (unity_MatrixV * unity_ObjectToWorld)
#define gl_ModelViewMatrixTranspose         (transpose(unity_MatrixV * unity_ObjectToWorld))
#define gl_ModelViewMatrixInverseTranspose  (transpose(unity_WorldToObject * unity_MatrixInvV))
#define gl_NormalMatrix                     (transpose(mat3(unity_WorldToObject * unity_MatrixInvV)))
#define gl_ProjectionMatrix                 glstate_matrix_projection

#if __VERSION__ < 120
#ifndef UNITY_GLSL_STRIP_TRANSPOSE
mat3 transpose(mat3 mtx)
{
    vec3 c0 = mtx[0];
    vec3 c1 = mtx[1];
    vec3 c2 = mtx[2];

    return mat3(
        vec3(c0.x, c1.x, c2.x),
        vec3(c0.y, c1.y, c2.y),
        vec3(c0.z, c1.z, c2.z)
    );
}
mat4 transpose(mat4 mtx)
{
    vec4 c0 = mtx[0];
    vec4 c1 = mtx[1];
    vec4 c2 = mtx[2];
    vec4 c3 = mtx[3];

    return mat4(
        vec4(c0.x, c1.x, c2.x, c3.x),
        vec4(c0.y, c1.y, c2.y, c3.y),
        vec4(c0.z, c1.z, c2.z, c3.z),
        vec4(c0.w, c1.w, c2.w, c3.w)
    );
}
#endif
#endif // __VERSION__ < 120

#endif // GLSL_SUPPORT_INCLUDED

#line 53

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif
/* UNITY: Original start of shader */

            // #pragma only_renderers gles3

            #ifdef SHADER_API_GLES3
            
            #endif

            uniform vec4 _UvTopLeftRight;
            uniform vec4 _UvBottomLeftRight;

            
            
            

#ifdef VERTEX
#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;


            out vec2 textureCoord;

            void main()
            {
                #ifdef SHADER_API_GLES3
                vec2 uvTop = mix(_UvTopLeftRight.xy, _UvTopLeftRight.zw, gl_MultiTexCoord0.x);
                vec2 uvBottom = mix(_UvBottomLeftRight.xy, _UvBottomLeftRight.zw, gl_MultiTexCoord0.x);
                textureCoord = mix(uvTop, uvBottom, gl_MultiTexCoord0.y);

                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                #endif
            }

            
#endif
#ifdef FRAGMENT
#define gl_FragColor _glesFragColor
layout(location = 0) out mediump vec4 _glesFragColor;

precision highp float;

            in vec2 textureCoord;
            uniform samplerExternalOES _MainTex;

            void main()
            {
                #ifdef SHADER_API_GLES3
                gl_FragColor = texture(_MainTex, textureCoord);
                #endif
            }

            
#endif

//////////////////////////////////////////////////////
Global Keywords: STEREO_INSTANCING_ON 
Local Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "gles3":
Shader Disassembly:
#version 300 es
#extension GL_OES_EGL_image_external_essl3 : require
#define UNITY_NO_DXT5nm 1
#define UNITY_NO_RGBM 1
#define UNITY_ENABLE_REFLECTION_BUFFERS 1
#define UNITY_FRAMEBUFFER_FETCH_AVAILABLE 1
#define UNITY_NO_CUBEMAP_ARRAY 1
#define UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS 1
#define UNITY_PBS_USE_BRDF1 1
#define UNITY_SPECCUBE_BOX_PROJECTION 1
#define UNITY_SPECCUBE_BLENDING 1
#define UNITY_ENABLE_DETAIL_NORMALMAP 1
#define SHADER_API_MOBILE 1
#define UNITY_LIGHTMAP_DLDR_ENCODING 1
#define STEREO_INSTANCING_ON 1
#ifndef SHADER_TARGET
    #define SHADER_TARGET 35
#endif
#ifndef SHADER_REQUIRE_INTERPOLATORS10
    #define SHADER_REQUIRE_INTERPOLATORS10 1
#endif
#ifndef SHADER_REQUIRE_INTERPOLATORS15
    #define SHADER_REQUIRE_INTERPOLATORS15 1
#endif
#ifndef SHADER_REQUIRE_INTEGERS
    #define SHADER_REQUIRE_INTEGERS 1
#endif
#ifndef SHADER_REQUIRE_MRT4
    #define SHADER_REQUIRE_MRT4 1
#endif
#ifndef SHADER_REQUIRE_DERIVATIVES
    #define SHADER_REQUIRE_DERIVATIVES 1
#endif
#ifndef SHADER_REQUIRE_SAMPLELOD
    #define SHADER_REQUIRE_SAMPLELOD 1
#endif
#ifndef SHADER_REQUIRE_FRAGCOORD
    #define SHADER_REQUIRE_FRAGCOORD 1
#endif
#ifndef SHADER_REQUIRE_2DARRAY
    #define SHADER_REQUIRE_2DARRAY 1
#endif
#ifndef SHADER_REQUIRE_INSTANCING
    #define SHADER_REQUIRE_INSTANCING 1
#endif
#ifndef SHADER_TARGET_AVAILABLE
    #define SHADER_TARGET_AVAILABLE 35
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS10
    #define SHADER_AVAILABLE_INTERPOLATORS10 1
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS15
    #define SHADER_AVAILABLE_INTERPOLATORS15 1
#endif
#ifndef SHADER_AVAILABLE_INTEGERS
    #define SHADER_AVAILABLE_INTEGERS 1
#endif
#ifndef SHADER_AVAILABLE_MRT4
    #define SHADER_AVAILABLE_MRT4 1
#endif
#ifndef SHADER_AVAILABLE_DERIVATIVES
    #define SHADER_AVAILABLE_DERIVATIVES 1
#endif
#ifndef SHADER_AVAILABLE_SAMPLELOD
    #define SHADER_AVAILABLE_SAMPLELOD 1
#endif
#ifndef SHADER_AVAILABLE_FRAGCOORD
    #define SHADER_AVAILABLE_FRAGCOORD 1
#endif
#ifndef SHADER_AVAILABLE_2DARRAY
    #define SHADER_AVAILABLE_2DARRAY 1
#endif
#ifndef SHADER_AVAILABLE_INSTANCING
    #define SHADER_AVAILABLE_INSTANCING 1
#endif
#ifndef SHADER_API_GLES3
    #define SHADER_API_GLES3 1
#endif
#line 1
#ifndef GLSL_SUPPORT_INCLUDED
#define GLSL_SUPPORT_INCLUDED

// Automatically included in raw GLSL (GLSLPROGRAM) shader snippets, to map from some of the legacy OpenGL
// variable names to uniform names used by Unity.

#ifdef GL_FRAGMENT_PRECISION_HIGH
    precision highp float;
#else
    precision mediump float;
#endif

uniform mat4 unity_ObjectToWorld;
uniform mat4 unity_WorldToObject;
uniform mat4 unity_MatrixVP;
uniform mat4 unity_MatrixV;
uniform mat4 unity_MatrixInvV;
uniform mat4 glstate_matrix_projection;

#define gl_ModelViewProjectionMatrix        (unity_MatrixVP * unity_ObjectToWorld)
#define gl_ModelViewMatrix                  (unity_MatrixV * unity_ObjectToWorld)
#define gl_ModelViewMatrixTranspose         (transpose(unity_MatrixV * unity_ObjectToWorld))
#define gl_ModelViewMatrixInverseTranspose  (transpose(unity_WorldToObject * unity_MatrixInvV))
#define gl_NormalMatrix                     (transpose(mat3(unity_WorldToObject * unity_MatrixInvV)))
#define gl_ProjectionMatrix                 glstate_matrix_projection

#if __VERSION__ < 120
#ifndef UNITY_GLSL_STRIP_TRANSPOSE
mat3 transpose(mat3 mtx)
{
    vec3 c0 = mtx[0];
    vec3 c1 = mtx[1];
    vec3 c2 = mtx[2];

    return mat3(
        vec3(c0.x, c1.x, c2.x),
        vec3(c0.y, c1.y, c2.y),
        vec3(c0.z, c1.z, c2.z)
    );
}
mat4 transpose(mat4 mtx)
{
    vec4 c0 = mtx[0];
    vec4 c1 = mtx[1];
    vec4 c2 = mtx[2];
    vec4 c3 = mtx[3];

    return mat4(
        vec4(c0.x, c1.x, c2.x, c3.x),
        vec4(c0.y, c1.y, c2.y, c3.y),
        vec4(c0.z, c1.z, c2.z, c3.z),
        vec4(c0.w, c1.w, c2.w, c3.w)
    );
}
#endif
#endif // __VERSION__ < 120

#endif // GLSL_SUPPORT_INCLUDED

#line 78

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif
/* UNITY: Original start of shader */

            // #pragma only_renderers gles3

            #ifdef SHADER_API_GLES3
            
            #endif

            uniform vec4 _UvTopLeftRight;
            uniform vec4 _UvBottomLeftRight;

            
            
            

#ifdef VERTEX
#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;


            out vec2 textureCoord;

            void main()
            {
                #ifdef SHADER_API_GLES3
                vec2 uvTop = mix(_UvTopLeftRight.xy, _UvTopLeftRight.zw, gl_MultiTexCoord0.x);
                vec2 uvBottom = mix(_UvBottomLeftRight.xy, _UvBottomLeftRight.zw, gl_MultiTexCoord0.x);
                textureCoord = mix(uvTop, uvBottom, gl_MultiTexCoord0.y);

                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                #endif
            }

            
#endif
#ifdef FRAGMENT
#define gl_FragColor _glesFragColor
layout(location = 0) out mediump vec4 _glesFragColor;

precision highp float;

            in vec2 textureCoord;
            uniform samplerExternalOES _MainTex;

            void main()
            {
                #ifdef SHADER_API_GLES3
                gl_FragColor = texture(_MainTex, textureCoord);
                #endif
            }

            
#endif

-- Hardware tier variant: Tier 1
-- Fragment shader for "gles3":
Shader Disassembly:
#version 300 es
#extension GL_OES_EGL_image_external_essl3 : require
#define UNITY_NO_DXT5nm 1
#define UNITY_NO_RGBM 1
#define UNITY_ENABLE_REFLECTION_BUFFERS 1
#define UNITY_FRAMEBUFFER_FETCH_AVAILABLE 1
#define UNITY_NO_CUBEMAP_ARRAY 1
#define UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS 1
#define UNITY_PBS_USE_BRDF1 1
#define UNITY_SPECCUBE_BOX_PROJECTION 1
#define UNITY_SPECCUBE_BLENDING 1
#define UNITY_ENABLE_DETAIL_NORMALMAP 1
#define SHADER_API_MOBILE 1
#define UNITY_LIGHTMAP_DLDR_ENCODING 1
#define STEREO_INSTANCING_ON 1
#ifndef SHADER_TARGET
    #define SHADER_TARGET 35
#endif
#ifndef SHADER_REQUIRE_INTERPOLATORS10
    #define SHADER_REQUIRE_INTERPOLATORS10 1
#endif
#ifndef SHADER_REQUIRE_INTERPOLATORS15
    #define SHADER_REQUIRE_INTERPOLATORS15 1
#endif
#ifndef SHADER_REQUIRE_INTEGERS
    #define SHADER_REQUIRE_INTEGERS 1
#endif
#ifndef SHADER_REQUIRE_MRT4
    #define SHADER_REQUIRE_MRT4 1
#endif
#ifndef SHADER_REQUIRE_DERIVATIVES
    #define SHADER_REQUIRE_DERIVATIVES 1
#endif
#ifndef SHADER_REQUIRE_SAMPLELOD
    #define SHADER_REQUIRE_SAMPLELOD 1
#endif
#ifndef SHADER_REQUIRE_FRAGCOORD
    #define SHADER_REQUIRE_FRAGCOORD 1
#endif
#ifndef SHADER_REQUIRE_2DARRAY
    #define SHADER_REQUIRE_2DARRAY 1
#endif
#ifndef SHADER_REQUIRE_INSTANCING
    #define SHADER_REQUIRE_INSTANCING 1
#endif
#ifndef SHADER_TARGET_AVAILABLE
    #define SHADER_TARGET_AVAILABLE 35
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS10
    #define SHADER_AVAILABLE_INTERPOLATORS10 1
#endif
#ifndef SHADER_AVAILABLE_INTERPOLATORS15
    #define SHADER_AVAILABLE_INTERPOLATORS15 1
#endif
#ifndef SHADER_AVAILABLE_INTEGERS
    #define SHADER_AVAILABLE_INTEGERS 1
#endif
#ifndef SHADER_AVAILABLE_MRT4
    #define SHADER_AVAILABLE_MRT4 1
#endif
#ifndef SHADER_AVAILABLE_DERIVATIVES
    #define SHADER_AVAILABLE_DERIVATIVES 1
#endif
#ifndef SHADER_AVAILABLE_SAMPLELOD
    #define SHADER_AVAILABLE_SAMPLELOD 1
#endif
#ifndef SHADER_AVAILABLE_FRAGCOORD
    #define SHADER_AVAILABLE_FRAGCOORD 1
#endif
#ifndef SHADER_AVAILABLE_2DARRAY
    #define SHADER_AVAILABLE_2DARRAY 1
#endif
#ifndef SHADER_AVAILABLE_INSTANCING
    #define SHADER_AVAILABLE_INSTANCING 1
#endif
#ifndef SHADER_API_GLES3
    #define SHADER_API_GLES3 1
#endif
#line 1
#ifndef GLSL_SUPPORT_INCLUDED
#define GLSL_SUPPORT_INCLUDED

// Automatically included in raw GLSL (GLSLPROGRAM) shader snippets, to map from some of the legacy OpenGL
// variable names to uniform names used by Unity.

#ifdef GL_FRAGMENT_PRECISION_HIGH
    precision highp float;
#else
    precision mediump float;
#endif

uniform mat4 unity_ObjectToWorld;
uniform mat4 unity_WorldToObject;
uniform mat4 unity_MatrixVP;
uniform mat4 unity_MatrixV;
uniform mat4 unity_MatrixInvV;
uniform mat4 glstate_matrix_projection;

#define gl_ModelViewProjectionMatrix        (unity_MatrixVP * unity_ObjectToWorld)
#define gl_ModelViewMatrix                  (unity_MatrixV * unity_ObjectToWorld)
#define gl_ModelViewMatrixTranspose         (transpose(unity_MatrixV * unity_ObjectToWorld))
#define gl_ModelViewMatrixInverseTranspose  (transpose(unity_WorldToObject * unity_MatrixInvV))
#define gl_NormalMatrix                     (transpose(mat3(unity_WorldToObject * unity_MatrixInvV)))
#define gl_ProjectionMatrix                 glstate_matrix_projection

#if __VERSION__ < 120
#ifndef UNITY_GLSL_STRIP_TRANSPOSE
mat3 transpose(mat3 mtx)
{
    vec3 c0 = mtx[0];
    vec3 c1 = mtx[1];
    vec3 c2 = mtx[2];

    return mat3(
        vec3(c0.x, c1.x, c2.x),
        vec3(c0.y, c1.y, c2.y),
        vec3(c0.z, c1.z, c2.z)
    );
}
mat4 transpose(mat4 mtx)
{
    vec4 c0 = mtx[0];
    vec4 c1 = mtx[1];
    vec4 c2 = mtx[2];
    vec4 c3 = mtx[3];

    return mat4(
        vec4(c0.x, c1.x, c2.x, c3.x),
        vec4(c0.y, c1.y, c2.y, c3.y),
        vec4(c0.z, c1.z, c2.z, c3.z),
        vec4(c0.w, c1.w, c2.w, c3.w)
    );
}
#endif
#endif // __VERSION__ < 120

#endif // GLSL_SUPPORT_INCLUDED

#line 78

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif

#line 14
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif
/* UNITY: Original start of shader */

            // #pragma only_renderers gles3

            #ifdef SHADER_API_GLES3
            
            #endif

            uniform vec4 _UvTopLeftRight;
            uniform vec4 _UvBottomLeftRight;

            
            
            

#ifdef VERTEX
#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;


            out vec2 textureCoord;

            void main()
            {
                #ifdef SHADER_API_GLES3
                vec2 uvTop = mix(_UvTopLeftRight.xy, _UvTopLeftRight.zw, gl_MultiTexCoord0.x);
                vec2 uvBottom = mix(_UvBottomLeftRight.xy, _UvBottomLeftRight.zw, gl_MultiTexCoord0.x);
                textureCoord = mix(uvTop, uvBottom, gl_MultiTexCoord0.y);

                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                #endif
            }

            
#endif
#ifdef FRAGMENT
#define gl_FragColor _glesFragColor
layout(location = 0) out mediump vec4 _glesFragColor;

precision highp float;

            in vec2 textureCoord;
            uniform samplerExternalOES _MainTex;

            void main()
            {
                #ifdef SHADER_API_GLES3
                gl_FragColor = texture(_MainTex, textureCoord);
                #endif
            }

            
#endif

 }
}
SubShader { 
 Pass {
  ZWrite Off
  //////////////////////////////////
  //                              //
  //      Compiled programs       //
  //                              //
  //////////////////////////////////
//////////////////////////////////////////////////////
Global Keywords: <none>
Local Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "glcore":
Set 2D Texture "_MainTex" to slot 0

Shader Disassembly:
#ifdef VERTEX
#version 150
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ARB_shader_bit_encoding
#extension GL_ARB_shader_bit_encoding : enable
#endif

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _UvTopLeftRight;
uniform 	vec4 _UvBottomLeftRight;
in  vec4 in_POSITION0;
in  vec2 in_TEXCOORD0;
out vec2 vs_TEXCOORD0;
vec4 u_xlat0;
vec4 u_xlat1;
vec2 u_xlat4;
void main()
{
    u_xlat0.xy = (-_UvBottomLeftRight.xy) + _UvBottomLeftRight.zw;
    u_xlat0.xy = in_TEXCOORD0.xx * u_xlat0.xy + _UvBottomLeftRight.xy;
    u_xlat4.xy = (-_UvTopLeftRight.xy) + _UvTopLeftRight.zw;
    u_xlat4.xy = in_TEXCOORD0.xx * u_xlat4.xy + _UvTopLeftRight.xy;
    u_xlat0.xy = (-u_xlat4.xy) + u_xlat0.xy;
    u_xlat0.xy = in_TEXCOORD0.yy * u_xlat0.xy + u_xlat4.xy;
    u_xlat0.z = (-u_xlat0.y) + 1.0;
    vs_TEXCOORD0.xy = u_xlat0.zx;
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
    return;
}

#endif
#ifdef FRAGMENT
#version 150
#extension GL_ARB_explicit_attrib_location : require
#ifdef GL_ARB_shader_bit_encoding
#extension GL_ARB_shader_bit_encoding : enable
#endif

#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
UNITY_LOCATION(0) uniform  sampler2D _MainTex;
in  vec2 vs_TEXCOORD0;
layout(location = 0) out vec4 SV_Target0;
void main()
{
    SV_Target0 = texture(_MainTex, vs_TEXCOORD0.xy);
    return;
}

#endif


-- Hardware tier variant: Tier 1
-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

//////////////////////////////////////////////////////
Global Keywords: STEREO_INSTANCING_ON 
Local Keywords: <none>
-- Hardware tier variant: Tier 1
-- Vertex shader for "glcore":
Set 2D Texture "_MainTex" to slot 0

Constant Buffer "UnityStereoGlobals" (1088 bytes) on slot 0 {
  Matrix4x4 unity_StereoMatrixP[2] at 0
  Matrix4x4 unity_StereoMatrixV[2] at 128
  Matrix4x4 unity_StereoMatrixInvV[2] at 256
  Matrix4x4 unity_StereoMatrixVP[2] at 384
  Matrix4x4 unity_StereoCameraProjection[2] at 512
  Matrix4x4 unity_StereoCameraInvProjection[2] at 640
  Matrix4x4 unity_StereoWorldToCamera[2] at 768
  Matrix4x4 unity_StereoCameraToWorld[2] at 896
  Vector3 unity_StereoWorldSpaceCameraPos[2] at 1024
  Vector4 unity_StereoScaleOffset[2] at 1056
}

Shader Disassembly:
#ifdef VERTEX
#version 330
#extension GL_ARB_explicit_attrib_location : require

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 _UvTopLeftRight;
uniform 	vec4 _UvBottomLeftRight;
UNITY_BINDING(0) uniform UnityStereoGlobals {
	vec4 hlslcc_mtx4x4unity_StereoMatrixP[8];
	vec4 hlslcc_mtx4x4unity_StereoMatrixV[8];
	vec4 hlslcc_mtx4x4unity_StereoMatrixInvV[8];
	vec4 hlslcc_mtx4x4unity_StereoMatrixVP[8];
	vec4 hlslcc_mtx4x4unity_StereoCameraProjection[8];
	vec4 hlslcc_mtx4x4unity_StereoCameraInvProjection[8];
	vec4 hlslcc_mtx4x4unity_StereoWorldToCamera[8];
	vec4 hlslcc_mtx4x4unity_StereoCameraToWorld[8];
	vec3 unity_StereoWorldSpaceCameraPos[2];
	vec4 unity_StereoScaleOffset[2];
};
in  vec4 in_POSITION0;
in  vec2 in_TEXCOORD0;
out vec2 vs_TEXCOORD0;
vec4 u_xlat0;
vec4 u_xlat1;
vec2 u_xlat4;
void main()
{
    u_xlat0.xy = (-_UvBottomLeftRight.xy) + _UvBottomLeftRight.zw;
    u_xlat0.xy = in_TEXCOORD0.xx * u_xlat0.xy + _UvBottomLeftRight.xy;
    u_xlat4.xy = (-_UvTopLeftRight.xy) + _UvTopLeftRight.zw;
    u_xlat4.xy = in_TEXCOORD0.xx * u_xlat4.xy + _UvTopLeftRight.xy;
    u_xlat0.xy = (-u_xlat4.xy) + u_xlat0.xy;
    u_xlat0.xy = in_TEXCOORD0.yy * u_xlat0.xy + u_xlat4.xy;
    u_xlat0.z = (-u_xlat0.y) + 1.0;
    vs_TEXCOORD0.xy = u_xlat0.zx;
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_StereoMatrixVP[1];
    u_xlat1 = hlslcc_mtx4x4unity_StereoMatrixVP[0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_StereoMatrixVP[2] * u_xlat0.zzzz + u_xlat1;
    gl_Position = hlslcc_mtx4x4unity_StereoMatrixVP[3] * u_xlat0.wwww + u_xlat1;
    return;
}

#endif
#ifdef FRAGMENT
#version 330
#extension GL_ARB_explicit_attrib_location : require

#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
UNITY_LOCATION(0) uniform  sampler2D _MainTex;
in  vec2 vs_TEXCOORD0;
layout(location = 0) out vec4 SV_Target0;
void main()
{
    SV_Target0 = texture(_MainTex, vs_TEXCOORD0.xy);
    return;
}

#endif


-- Hardware tier variant: Tier 1
-- Fragment shader for "glcore":
Shader Disassembly:
// All GLSL source is contained within the vertex program

 }
}
Fallback Off
}